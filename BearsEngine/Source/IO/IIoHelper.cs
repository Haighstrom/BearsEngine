﻿using BearsEngine.Source.Tools.IO.CSV;
using BearsEngine.Source.Tools.IO.FileDirectory;
using BearsEngine.Source.Tools.IO.JSON;
using BearsEngine.Source.Tools.IO.TXT;
using BearsEngine.Source.Tools.IO.XML;

namespace BearsEngine.IO;

internal interface IIoHelper : ITxtFileIoHelper, ICsvFileIoHelper, IJsonFileIoHelper, IXmlFileIoHelper, IFileDirectoryIoHelper
{
}
