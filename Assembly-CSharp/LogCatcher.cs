using System;
using UnityEngine;

// Token: 0x02000925 RID: 2341
public class LogCatcher : ILogHandler
{
	// Token: 0x0600414B RID: 16715 RVA: 0x0016EEE8 File Offset: 0x0016D0E8
	public LogCatcher(ILogHandler old)
	{
		this.def = old;
	}

	// Token: 0x0600414C RID: 16716 RVA: 0x0016EEF8 File Offset: 0x0016D0F8
	void ILogHandler.LogException(Exception exception, global::UnityEngine.Object context)
	{
		string text = exception.ToString();
		string text2 = ((context != null) ? context.ToString() : null);
		if (text == "False" || text2 == "False")
		{
			global::Debug.LogError("False only message!");
		}
		this.def.LogException(exception, context);
	}

	// Token: 0x0600414D RID: 16717 RVA: 0x0016EF4E File Offset: 0x0016D14E
	void ILogHandler.LogFormat(LogType logType, global::UnityEngine.Object context, string format, params object[] args)
	{
		if (string.Format(format, args) == "False")
		{
			global::Debug.LogError("False only message!");
		}
		this.def.LogFormat(logType, context, format, args);
	}

	// Token: 0x040028D3 RID: 10451
	private ILogHandler def;
}
