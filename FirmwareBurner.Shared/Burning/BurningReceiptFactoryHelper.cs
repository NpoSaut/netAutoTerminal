using System;
using FirmwareBurner.Imaging;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Burning
{
    /// <summary>����� ���������� ��� ����������� ������ �������� ����
    ///     <see cref="BurningReceiptFactory{TImage,TBurningToolFacadeFactory}" />
    /// </summary>
    public static class BurningReceiptFactoryHelper
    {
        /// <summary>������������ � ��������� ���������� ������� �������� ����
        ///     <see cref="BurningReceiptFactory{TImage,TBurningToolFacadeFactory}" />, ������������
        ///     <typeparamref name="TBurningToolFacadeFactory" /> ��� ���������� ������� � ������ ��-���������</summary>
        /// <typeparam name="TImage">��� ������</typeparam>
        /// <typeparam name="TBurningToolFacadeFactory">��� ������� ������������ ��� ���������� ������</typeparam>
        /// <param name="Container">���������, � ������� ����� ���������������� �������</param>
        public static IUnityContainer RegisterBurningReceiptFactory<TImage, TBurningToolFacadeFactory>(this IUnityContainer Container)
            where TImage : IImage
            where TBurningToolFacadeFactory : class, IBurningToolFacadeFactory<TImage>
        {
            return RegisterBurningReceiptFactory<TImage, TBurningToolFacadeFactory>(Container, typeof (TBurningToolFacadeFactory).FullName);
        }

        /// <summary>������������ � ��������� ���������� ������� �������� ����
        ///     <see cref="BurningReceiptFactory{TImage,TBurningToolFacadeFactory}" />, ������������
        ///     <typeparamref name="TBurningToolFacadeFactory" /> ��� ���������� �������</summary>
        /// <typeparam name="TImage">��� ������</typeparam>
        /// <typeparam name="TBurningToolFacadeFactory">��� ������� ������������ ��� ���������� ������</typeparam>
        /// <param name="Container">���������, � ������� ����� ���������������� �������</param>
        /// <param name="Name">���, � ������� ����� ���������������� �������</param>
        public static IUnityContainer RegisterBurningReceiptFactory<TImage, TBurningToolFacadeFactory>(this IUnityContainer Container, String Name)
            where TImage : IImage
            where TBurningToolFacadeFactory : class, IBurningToolFacadeFactory<TImage>
        {
            return Container.RegisterType<IBurningReceiptFactory,
                BurningReceiptFactory<TImage, TBurningToolFacadeFactory>>(Name, new ContainerControlledLifetimeManager());
        }
    }
}