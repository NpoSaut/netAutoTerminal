using System;
using System.Collections.Generic;

namespace FirmwareBurner.Burning
{
    public interface IBurningToolFacadeFactory<in TImage>
    {
        IBurningToolFacade<TImage> GetBurningToolFacade(String DeviceName);
    }
}
