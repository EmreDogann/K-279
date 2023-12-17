namespace Utils
{
    // A simple wrapper class for fields that is intended to be used mainly for coroutines which don't support 'ref' or 'out' parameters.
    // Allows you to pass those fields as "by reference" by simply wrapping it as a class instead.
    public class Ref<T>
    {
        public T Value { get; set; }

        public Ref() {}

        public Ref(T reference)
        {
            Value = reference;
        }
    }
}