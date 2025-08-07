import os
import re
import json

# --- 配置区域 ---
# 【必须】请修改以下路径，使其指向您反编译出的对应文件
SIMHASHES_PATH = "/root/date/oni/Assembly-CSharp/SimHashes.cs"
TUNING_PATH = "D:/path/to/decompiled/source/TUNING.cs"

# 输出的词汇表文件名
OUTPUT_GLOSSARY_FILE = "auto_glossary.json"

def extract_from_simhashes(file_path):
    """从SimHashes.cs中提取枚举成员。"""
    print(f"正在处理 {file_path}...")
    if not os.path.exists(file_path):
        print(f"错误：文件未找到 - {file_path}")
        return {}
    
    glossary = {}
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()

    # 正则表达式匹配枚举成员，如 Water, Steel, Oxygen 等
    pattern = re.compile(r"^\s*([A-Z][A-Za-z0-9_]+)\s*=", re.MULTILINE)
    matches = pattern.findall(content)

    for name in matches:
        full_name = f"SimHashes.{name}"
        glossary[full_name] = f"一个核心的游戏元素ID，代表'{name}'。"
    
    print(f"从SimHashes中提取了 {len(glossary)} 个术语。")
    return glossary

def extract_from_tuning(file_path):
    """从TUNING.cs中提取常量。这是一个简化的提取，专注于关键数值。"""
    print(f"正在处理 {file_path}...")
    if not os.path.exists(file_path):
        print(f"错误：文件未找到 - {file_path}")
        return {}
        
    glossary = {}
    with open(file_path, 'r', encoding='utf-8') as f:
        lines = f.readlines()
        
    current_path = ["TUNING"]
    # 这是一个简化的、基于缩进和括号的路径追踪，不完美但有效
    for line in lines:
        stripped_line = line.strip()
        
        # 更新路径
        if "public static class" in stripped_line:
            class_name = stripped_line.split("public static class")[1].strip()
            # 假设层级不会回退得太复杂
            if len(current_path) > 1 and "}" in lines[lines.index(line)-1]:
                 current_path.pop()
            current_path.append(class_name)

        if "}" in stripped_line:
            if len(current_path) > 1:
                current_path.pop()

        # 提取常量
        if stripped_line.startswith("public static readonly"):
            match = re.search(r"public\s+static\s+readonly\s+[\w\.<>\[\]]+\s+([\w_]+)\s*=", stripped_line)
            if match:
                const_name = match.group(1)
                full_name = ".".join(current_path) + "." + const_name
                glossary[full_name] = f"一个游戏平衡性常量，用于定义'{const_name}'。"

    print(f"从TUNING中提取了 {len(glossary)} 个术语。")
    return glossary

def generate_glossary():
    """主函数，执行提取并保存结果。"""
    print("开始生成自动化词汇表...")
    
    # 从各文件提取
    simhashes_terms = extract_from_simhashes(SIMHASHES_PATH)
    tuning_terms = extract_from_tuning(TUNING_PATH)
    
    # 合并结果
    final_glossary = {**simhashes_terms, **tuning_terms}
    
    # 保存到JSON文件
    with open(OUTPUT_GLOSSARY_FILE, 'w', encoding='utf-8') as f:
        json.dump(final_glossary, f, indent=2, ensure_ascii=False)
        
    print(f"\n成功！自动化词汇表已保存到 '{OUTPUT_GLOSSARY_FILE}'")
    print(f"总共包含 {len(final_glossary)} 个术语。")

if __name__ == "__main__":
    generate_glossary()
