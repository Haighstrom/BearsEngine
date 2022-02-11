namespace BearsEngine
{
    public interface IAttachable<T>
    {
        void AttachTo(T t);
        void Dettach();
    }
}