using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000433 RID: 1075
public class CodeWriter
{
	// Token: 0x06001627 RID: 5671 RVA: 0x0007E2EB File Offset: 0x0007C4EB
	public CodeWriter(string path)
	{
		this.Path = path;
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x0007E305 File Offset: 0x0007C505
	public void Comment(string text)
	{
		this.Lines.Add("// " + text);
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x0007E320 File Offset: 0x0007C520
	public void BeginPartialClass(string class_name, string parent_name = null)
	{
		string text = "public partial class " + class_name;
		if (parent_name != null)
		{
			text = text + " : " + parent_name;
		}
		this.Line(text);
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x0007E36C File Offset: 0x0007C56C
	public void BeginClass(string class_name, string parent_name = null)
	{
		string text = "public class " + class_name;
		if (parent_name != null)
		{
			text = text + " : " + parent_name;
		}
		this.Line(text);
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x0007E3B5 File Offset: 0x0007C5B5
	public void EndClass()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x0007E3D0 File Offset: 0x0007C5D0
	public void BeginNameSpace(string name)
	{
		this.Line("namespace " + name);
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x0007E3FC File Offset: 0x0007C5FC
	public void EndNameSpace()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x0007E417 File Offset: 0x0007C617
	public void BeginArrayStructureInitialization(string name)
	{
		this.Line("new " + name);
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x0007E443 File Offset: 0x0007C643
	public void EndArrayStructureInitialization(bool last_item)
	{
		this.Indent--;
		if (!last_item)
		{
			this.Line("},");
			return;
		}
		this.Line("}");
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x0007E46D File Offset: 0x0007C66D
	public void BeginArraArrayInitialization(string array_type, string array_name)
	{
		this.Line(array_name + " = new " + array_type + "[]");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x0007E49F File Offset: 0x0007C69F
	public void EndArrayArrayInitialization(bool last_item)
	{
		this.Indent--;
		if (last_item)
		{
			this.Line("}");
			return;
		}
		this.Line("},");
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x0007E4C9 File Offset: 0x0007C6C9
	public void BeginConstructor(string name)
	{
		this.Line("public " + name + "()");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x0007E4FA File Offset: 0x0007C6FA
	public void EndConstructor()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x0007E515 File Offset: 0x0007C715
	public void BeginArrayAssignment(string array_type, string array_name)
	{
		this.Line(array_name + " = new " + array_type + "[]");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x0007E547 File Offset: 0x0007C747
	public void EndArrayAssignment()
	{
		this.Indent--;
		this.Line("};");
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x0007E562 File Offset: 0x0007C762
	public void FieldAssignment(string field_name, string value)
	{
		this.Line(field_name + " = " + value + ";");
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x0007E57B File Offset: 0x0007C77B
	public void BeginStructureDelegateFieldInitializer(string name)
	{
		this.Line(name + "=delegate()");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x0007E5A7 File Offset: 0x0007C7A7
	public void EndStructureDelegateFieldInitializer()
	{
		this.Indent--;
		this.Line("},");
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x0007E5C2 File Offset: 0x0007C7C2
	public void BeginIf(string condition)
	{
		this.Line("if(" + condition + ")");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x0007E5F4 File Offset: 0x0007C7F4
	public void BeginElseIf(string condition)
	{
		this.Indent--;
		this.Line("}");
		this.Line("else if(" + condition + ")");
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x0007E649 File Offset: 0x0007C849
	public void EndIf()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x0007E664 File Offset: 0x0007C864
	public void BeginFunctionDeclaration(string name, string parameter, string return_type)
	{
		this.Line(string.Concat(new string[] { "public ", return_type, " ", name, "(", parameter, ")" }));
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x0007E6C8 File Offset: 0x0007C8C8
	public void BeginFunctionDeclaration(string name, string return_type)
	{
		this.Line(string.Concat(new string[] { "public ", return_type, " ", name, "()" }));
		this.Line("{");
		this.Indent++;
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x0007E71F File Offset: 0x0007C91F
	public void EndFunctionDeclaration()
	{
		this.Indent--;
		this.Line("}");
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x0007E73C File Offset: 0x0007C93C
	private void InternalNamedParameter(string name, string value, bool last_parameter)
	{
		string text = "";
		if (!last_parameter)
		{
			text = ",";
		}
		this.Line(name + ":" + value + text);
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x0007E76B File Offset: 0x0007C96B
	public void NamedParameterBool(string name, bool value, bool last_parameter = false)
	{
		this.InternalNamedParameter(name, value.ToString().ToLower(), last_parameter);
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x0007E781 File Offset: 0x0007C981
	public void NamedParameterInt(string name, int value, bool last_parameter = false)
	{
		this.InternalNamedParameter(name, value.ToString(), last_parameter);
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x0007E792 File Offset: 0x0007C992
	public void NamedParameterFloat(string name, float value, bool last_parameter = false)
	{
		this.InternalNamedParameter(name, value.ToString() + "f", last_parameter);
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x0007E7AD File Offset: 0x0007C9AD
	public void NamedParameterString(string name, string value, bool last_parameter = false)
	{
		this.InternalNamedParameter(name, value, last_parameter);
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x0007E7B8 File Offset: 0x0007C9B8
	public void BeginFunctionCall(string name)
	{
		this.Line(name);
		this.Line("(");
		this.Indent++;
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x0007E7DA File Offset: 0x0007C9DA
	public void EndFunctionCall()
	{
		this.Indent--;
		this.Line(");");
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x0007E7F8 File Offset: 0x0007C9F8
	public void FunctionCall(string function_name, params string[] parameters)
	{
		string text = function_name + "(";
		for (int i = 0; i < parameters.Length; i++)
		{
			text += parameters[i];
			if (i != parameters.Length - 1)
			{
				text += ", ";
			}
		}
		this.Line(text + ");");
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x0007E84E File Offset: 0x0007CA4E
	public void StructureFieldInitializer(string field, string value)
	{
		this.Line(field + " = " + value + ",");
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x0007E868 File Offset: 0x0007CA68
	public void StructureArrayFieldInitializer(string field, string field_type, params string[] values)
	{
		string text = field + " = new " + field_type + "[]{ ";
		for (int i = 0; i < values.Length; i++)
		{
			text += values[i];
			if (i < values.Length - 1)
			{
				text += ", ";
			}
		}
		text += " },";
		this.Line(text);
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x0007E8C8 File Offset: 0x0007CAC8
	public void Line(string text = "")
	{
		for (int i = 0; i < this.Indent; i++)
		{
			text = "\t" + text;
		}
		this.Lines.Add(text);
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x0007E8FF File Offset: 0x0007CAFF
	public void Flush()
	{
		File.WriteAllLines(this.Path, this.Lines.ToArray());
	}

	// Token: 0x04000D13 RID: 3347
	private List<string> Lines = new List<string>();

	// Token: 0x04000D14 RID: 3348
	private string Path;

	// Token: 0x04000D15 RID: 3349
	private int Indent;
}
