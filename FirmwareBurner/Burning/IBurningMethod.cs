using System;

namespace FirmwareBurner.Burning
{
    public interface IBurningMethod
    {
        String Name { get; }
        IBurningReceipt Receipt { get; }
    }
}
