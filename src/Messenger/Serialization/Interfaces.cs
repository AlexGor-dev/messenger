using System;
using System.Collections.Generic;
using System.Text;

namespace Complex.Serialization
{
    public interface IData : IEnumerable<IEntry>
    {
        object this[string name] { get; set; }
        object this[string name, object defaultValue] { get; }
    }

    public interface IEntry
    {
        string Name { get; }
        object Value { get; }
    }

    public interface IDataReader
    {
        bool ReadElement();
        object ReadAttribute(string name);
        string Name { get; }
        object Value { get; }
        bool EndOfElement { get; }
    }

    public interface IDataWriter
    {
        void BeginWrite();
        void WriteElement(string name, object value);
        void WriteStartElement(string name);
        void WriteAttribute(string name, object value);
        void WriteEndElement();
        void EndWrite();
    }

    public interface ISerializable
    {
        void SaveClassData(IData data);
        void LoadClassData(IData data);
    }

}
