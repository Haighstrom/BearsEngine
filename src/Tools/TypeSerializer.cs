using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BearsEngine
{
    [Serializable]
    public class SerializableType
    {
        [XmlIgnore]
        public Type Type { get; set; }

        public string TypeName
        {
            get => Type.AssemblyQualifiedName;
            set => Type = Type.GetType(value);
        }
    }
}
