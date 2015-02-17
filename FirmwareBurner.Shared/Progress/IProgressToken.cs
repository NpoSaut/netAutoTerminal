using System;

namespace FirmwareBurner.Progress
{
    public interface IProgressToken
    {
        void Start();
        void SetProgress(Double Progress);
        void SetToIntermediate();
        void OnCompleated();
    }
}