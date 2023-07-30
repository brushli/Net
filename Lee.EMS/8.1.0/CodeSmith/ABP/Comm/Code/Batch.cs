<script runat="template">
private string outputDirectory = String.Empty;

[Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
[Optional]
[Category("Output")]
[Description("生成的文件目录.")]
public string OutputDirectory
{
	get
	{
		if (outputDirectory.Length == 0) return this.CodeTemplateInfo.DirectoryName + "Output\\";
		
		return outputDirectory;
	}
	set
	{
		if (!value.EndsWith("\\")) value += "\\";
		
		outputDirectory = value;
	}
}
</script>