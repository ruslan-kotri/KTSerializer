Why this project was started.

At first, I needed a quick, but rather robust serializer to store data between application sessions.
.NET ProtoBuf serializer by Marc Gravell was my initial choice because it was quick enough, produced small data output and was open-source. Actually, it's still used by many of my projects. Not a bad thing, after all.


Why then to change things that work?

Because some issues with protobuf (I do not know if they come from initial Google design) were inappropriate, at my sight.

1.Intensive usage of integers in classes and fields/properties decsriptions is an additional burden on a developer. I am to always keep in memory what was the last counter used in current class for not to intercept with removed properties for not to get into trouble with data stored in files. And my automation creation of indexes for inherited classes (well, I was not so good with protobuf then) even now prevents me from inserting additional classes in the middle of the chain in many places.
Sometimes, even when I add a valid indexer for new property, data restore breaks for some reason. Only if I skip some numbers (which seem to correlate with some parallel, but unrelated, classes), things begin to work.

2.What worse, protobuf does not store 'false' and '0' (zero) at serialization. It means that if I have a property which is true by default, it will be always restored as true. Workaround is possible (and I have done it), but it was... embarrassing.

3.Work with <object> type was quite complicated, when not impossible. I had to serialize object values to strings and deserialize them from strings at restore (fortunately, my code allowed it rather easily, but it was a sheer luck).

(All this is stated at first half of year 2015. Things may have changed since, I have not proved it since.)


So the purposes of the desired serializer were:

1. Allow to easily make changes in inheritance chain (add or remove levels).
2. Allow move properties/fields between classes in the same inheritance chain.
3. Allow to change property to field and vice verse.
4. Allow to add or remove properties/fields without any impact on data restore (except, of course loosing data at removing - which is normal).
5. Allow to change type of property/field. Data should be changed accordingly at restore.
6. Private properties/fields should be serializable as well as public ones.
7. Allow to store properties/fields of 'object' type.
8. Allow to serialize/deserialize collections and collections of collections.
9. Store all data of all known types.
10. Be quick, ideally as quick as protobuf.
11. Do not consume too much hard drive space.


What was achieved?
Well, speed sometimes is as good as with protobuf, sometimes slower (20-40%) - it depends mainly on the types of the data serialized/deserialized. Files are 1.5-2 times bigger in size than by protobuf (mainly because I store information about initial type of the current piece of data with it). All other points, and conversions at first line, are supposed to be ready.
All primitive types plus Color type are serializable. KTSerialize can handle IList<T>, IDictionary<K,V>, arrays (T[], T[][]), collections of collections of these types and so on. [Unfortunately, I failed to find an easy way to directly serialize some useful types like HashSet<T>, so in order to use such type ony should apply appropriate OnSerialize and OnDeserialize actions and store in List or so.]

KT Serializer uses GUIDs to decorate stored classes, properties and fields. It allows to change inheritance chain of classes and move properties and fields between classes.


Known issues.
1. Direct serialization of collections. - I use a wrapper over such collections.


How to use?
There are plenty of examples in the test (with comments; actually, tests are supposed to cover serializer functionality as much as possible), but main line is:

1. Decorate all necessary classes with KTSerialize attribute.
2. Decorate all necessary properties and fields in classes from the step 1 with KTSerializeInclude attribute.
3. Use, if necessary, attributes (KTBeforeSerializeAttribute and so like) for methods which are to be called before/after serialization/deserialization.
4. Create new instance of KTSerializer. There is an option there, either to use processed types or not.
The common behaviour is to use previously processed types (classes) and store information about them in a special header stream. In my work projects it's either a MemoryStream (when working with WCF) or a FileStream (when working with NoSQL DB). This header stream contains metadata about already processed types, so they can be use over and over again. Of course, it should be stored somewhere between serialization sessions.
If I need serializer per case only (which is rare), useProcessedTypes can be set to null. Header stream must be null in this case.
5. Register types in serializer. It's not actually necessary because serializer starts processing types from the type of the sent object and tries to process metadata about all types it finds, but I prefer to be sure that necessary class is known.
6. Call .Serialize() - to serialize object to stream; .Deserialize<T>() - to deserialize object from stream to given type; or .Clone() - to clone object by using serializaion and deserialization on provided object.

There are also other options possible, of course, but this is the main way.


Code example (see it also in ExampleTest.cs file in tests project):

    [KTSerialize("c40c7c35-c4d5-4964-9996-943435e32336")]
    class AOne
    {
        [KTSerializeInclude("f7363d24-d6a9-485c-9e44-e5c4e64e816d")]
        private int iField = 0;


        private int _iProperty;
        [KTSerializeInclude("a1067eed-5653-410b-9960-93636bf70c94")]
        public int iProperty
        {
            get { return _iProperty; }
            set
            {
                _iProperty = value;
                iField = value * 2;
            }
        }


        public int notSerializedField = 100;
    }

        public void ExampleMethod()
        {
            KTSerializer serializer = new KTSerializer(true);

            AOne o1, o2;

            o1 = new AOne()
            {
                iProperty = 10,
                notSerializedField = 20
            };

            using (MemoryStream headerStream = new MemoryStream())
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, o1, headerStream);

                headerStream.Position = 0;
                stream.Position = 0;

                o2 = serializer.Deserialize<AOne>(stream, headerStream);
            }
        }

This piece of code stores serialized info in two streams - one stream for header and one for data. These streams are typically saved to files between sessions.



Disclaimer: This code is provided "as is", without guarantee of any sort. Use it at your own risk.
