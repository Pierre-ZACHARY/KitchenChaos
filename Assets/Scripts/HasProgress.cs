
using System;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;

    public class OnProgressChangeEventArgs
    {
        public float normalized_progress;
    }
}