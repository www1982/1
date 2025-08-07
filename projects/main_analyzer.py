import os
import json
import re
from google.generativeai.client import configure
from google.generativeai.generative_models import GenerativeModel
import time
import sys
import google.api_core.exceptions

# --- 1. 配置区域 ---
# 【必须】请将此路径修改为您反编译后存放.cs文件的根目录
DECOMPILED_CS_ROOT_DIR = "/root/date/oni/Assembly-CSharp" 
# 【必须】最终生成的JSON文件存放目录
JSON_OUTPUT_DIR = "oni_api_docs_output" 

# 从环境变量中获取API密钥
try:
    GOOGLE_API_KEY = os.environ['GOOGLE_API_KEY']
    configure(api_key=GOOGLE_API_KEY)
except KeyError:
    print("\n错误：请先设置 GOOGLE_API_KEY 环境变量！")
    print("Windows: setx GOOGLE_API_KEY \"YOUR_API_KEY\"")
    print("macOS/Linux: export GOOGLE_API_KEY=\"YOUR_API_KEY\"")
    print("(设置后需要重启终端或IDE)\n")
    sys.exit()

# --- 2. 静态知识库与加载 ---

# 手动编写的高层背景知识
MANUAL_PREAMBLE = """
# 背景知识：关于游戏《缺氧》(Oxygen Not Included)
这是一个关于太空殖民地生存和物理模拟的深度游戏。Modding的核心是与游戏世界进行交互。
- **核心单位**: 'Duplicant' (小人) 是执行任务的单位，有各种需求和属性。
- **核心资源**: 游戏围绕着气体、液体、固体的管理，以及温度和电力的控制。
- **核心概念**: 装饰度(Decor)影响小人心情；过热(Overheating)会损坏建筑；病菌(Germs)会引发疾病。
- **关键代码模式**:
  - 以 `...Config` 结尾的类通常是建筑、植物或生物的配置文件，是Modding的入口点。
  - `GameStateMachine` 用于定义实体（如小人、生物）的行为状态机。
  - `ElementConverter` 是实现物质转换（如水变蒸汽）的关键组件。
"""

AUTO_GLOSSARY_PATH = "auto_glossary.json" # 自动生成的词汇表路径

def load_glossary(file_path):
    """加载自动生成的词汇表。"""
    print("正在加载自动化词汇表...")
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            return json.load(f)
    except FileNotFoundError:
        print(f"警告：词汇表文件 '{file_path}' 未找到。将仅使用手动编写的背景知识。")
        return {}
    except json.JSONDecodeError:
        print(f"错误：词汇表文件 '{file_path}' 格式不正确。")
        return {}

def find_cs_files(root_dir):
    """查找所有.cs文件。"""
    cs_files = []
    for root, _, files in os.walk(root_dir):
        for file in files:
            if file.endswith('.cs'):
                cs_files.append(os.path.join(root, file))
    return cs_files

# --- 3. 核心分析函数 (增强版) ---

def analyze_code_with_enhanced_context(cs_file_path, glossary, manual_preamble):
    """使用组合的上下文来分析代码。"""
    try:
        with open(cs_file_path, 'r', encoding='utf-8') as f:
            class_code = f.read()
    except Exception as e:
        print(f"  !! 无法读取文件 {cs_file_path}: {e}")
        return None

    if len(class_code.strip()) < 50:
        print("  -- 文件内容过短，跳过分析。")
        return None

    class_name = os.path.splitext(os.path.basename(cs_file_path))[0]
    print(f"  正在使用增强上下文分析类: {class_name}...")

    # 【智能注入】在代码中查找相关的术语
    relevant_terms_info = ""
    for term, description in glossary.items():
        if re.search(r'\b' + re.escape(term) + r'\b', class_code):
            relevant_terms_info += f"- **{term}**: {description}\n"

    if not relevant_terms_info:
        relevant_terms_info = "此文件中未直接引用词汇表中的关键常量。"

    # 【组装最终Prompt】
    final_prompt = f"""
    你是一名顶级的C#游戏开发专家，并且是《缺氧》游戏的Modding大师。
    你的任务是分析下面提供的C#类代码，并以严格的JSON格式返回其API文档。

    ---
    # 核心背景知识
    {manual_preamble}
    ---
    # 在此文件中检测到的相关常量解释
    {relevant_terms_info}
    ---

    现在，请结合以上所有背景知识，对下面的代码进行深入、准确的分析。
    请确保你的描述能够体现出这些知识，使其对Mod开发者更有价值。

    {{
      "className": "...",
      "namespace": "...",
      "baseClass": "...",
      "interfaces": [...],
      "summary": "...",
      "keyMethods": [...],
      "keyProperties": [...]
    }}

    C#代码如下：
    ```csharp
    {class_code}
    ```
    """

    model = GenerativeModel('gemini-1.5-flash')
    safety_settings = [
        {"category": "HARM_CATEGORY_HARASSMENT", "threshold": "BLOCK_NONE"},
        {"category": "HARM_CATEGORY_HATE_SPEECH", "threshold": "BLOCK_NONE"},
        {"category": "HARM_CATEGORY_SEXUALLY_EXPLICIT", "threshold": "BLOCK_NONE"},
        {"category": "HARM_CATEGORY_DANGEROUS_CONTENT", "threshold": "BLOCK_NONE"},
    ]

    max_retries = 5
    initial_delay = 5  # 初始延迟（秒）

    for attempt in range(max_retries):
        json_text = ""
        try:
            response = model.generate_content(final_prompt, safety_settings=safety_settings)
            # 优先匹配带json标识的完整代码块
            json_text_match = re.search(r"```json\s*\n(.*?)\n```", response.text, re.DOTALL)
            if json_text_match:
                json_text = json_text_match.group(1)
            else:
                # 如果没有找到，就尝试从文本中找到第一个 { 和最后一个 } 之间的内容
                first_brace = response.text.find('{')
                last_brace = response.text.rfind('}')
                if first_brace != -1 and last_brace != -1 and last_brace > first_brace:
                    json_text = response.text[first_brace:last_brace+1]
                else:
                     # 如果还是找不到，就认为失败
                    print(f"  !! 在API响应中未找到有效的JSON内容。响应全文: {response.text}")
                    # 这种错误通常不是由速率限制引起的，因此直接返回None，不重试
                    return None
            
            return json.loads(json_text)

        except google.api_core.exceptions.ResourceExhausted as e:
            # 从错误消息中提取建议的延迟时间
            wait_match = re.search(r'retry_delay {\s*seconds: (\d+)\s*}', str(e))
            if wait_match:
                wait_time = int(wait_match.group(1)) + 1 # 加1秒作为缓冲
                print(f"  !! API速率限制。根据API建议，等待 {wait_time} 秒后重试... (尝试 {attempt + 1}/{max_retries})")
            else:
                wait_time = initial_delay * (2 ** attempt) # 指数退避
                print(f"  !! API速率限制。将在 {wait_time} 秒后重试... (尝试 {attempt + 1}/{max_retries})")
            
            time.sleep(wait_time)

        except json.JSONDecodeError as e:
            print(f"  !! JSON解析失败: {e}. 原始文本: '{json_text}'")
            # JSON解析失败是确定性错误，不应重试
            return None
        except Exception as e:
            print(f"  !! Gemini API调用或处理失败: {e}")
            # 对于其他未知错误，也进行重试
            wait_time = initial_delay * (2 ** attempt)
            print(f"  >> 发生未知错误，将在 {wait_time} 秒后重试... (尝试 {attempt + 1}/{max_retries})")
            time.sleep(wait_time)

    print(f"  × 在达到最大重试次数后，文件 {os.path.basename(cs_file_path)} 仍然处理失败。")
    return None

# --- 4. 主执行流程 (修改版) ---

def main():
    if 'GOOGLE_API_KEY' not in os.environ:
        print("错误：请先设置 GOOGLE_API_KEY 环境变量！")
        sys.exit()

    glossary = load_glossary(AUTO_GLOSSARY_PATH)

    cs_files_to_process = find_cs_files(DECOMPILED_CS_ROOT_DIR)
    if not cs_files_to_process:
        return

    # 【新】根据文件大小从大到小排序
    print("正在根据文件大小对文件进行排序...")
    try:
        # 使用 os.path.getsize 获取文件大小并降序排序
        cs_files_to_process.sort(key=os.path.getsize, reverse=True)
        print("文件排序完成。")
    except Exception as e:
        print(f"警告：文件排序时发生错误: {e}。将按默认顺序处理。")

    if not os.path.exists(JSON_OUTPUT_DIR):
        os.makedirs(JSON_OUTPUT_DIR)

    total_files = len(cs_files_to_process)
    print(f"\n准备开始使用增强上下文处理 {total_files} 个文件...")

    for i, cs_path in enumerate(cs_files_to_process):
        class_name = os.path.splitext(os.path.basename(cs_path))[0]
        relative_path = os.path.relpath(cs_path, DECOMPILED_CS_ROOT_DIR)
        json_filename = os.path.splitext(relative_path)[0] + ".json"
        output_json_path = os.path.join(JSON_OUTPUT_DIR, json_filename)

        if os.path.exists(output_json_path):
            print(f"  文件 {output_json_path} 已存在，跳过。")
            continue

        print(f"\n--- 处理进度: {i+1}/{total_files} | 文件: {class_name}.cs ---")

        api_doc = analyze_code_with_enhanced_context(cs_path, glossary, MANUAL_PREAMBLE)

        if api_doc:
            # 在写入前确保输出目录存在
            output_dir = os.path.dirname(output_json_path)
            os.makedirs(output_dir, exist_ok=True)
            
            with open(output_json_path, 'w', encoding='utf-8') as f:
                json.dump(api_doc, f, indent=2, ensure_ascii=False)
            print(f"  √ API文档已保存到 {output_json_path}")
        else:
            print(f"  × 未能为文件 {class_name}.cs 生成文档。")

if __name__ == "__main__":
    main()
