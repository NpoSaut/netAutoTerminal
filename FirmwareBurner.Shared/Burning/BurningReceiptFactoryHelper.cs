using System;
using FirmwareBurner.Imaging;
using Microsoft.Practices.Unity;

namespace FirmwareBurner.Burning
{
    /// <summary>Класс помощников для регистрации фабрик рецептов типа
    ///     <see cref="BurningReceiptFactory{TImage,TBurningToolFacadeFactory}" />
    /// </summary>
    public static class BurningReceiptFactoryHelper
    {
        /// <summary>Регистрирует в указанном контейнере фабрику рецептов типа
        ///     <see cref="BurningReceiptFactory{TImage,TBurningToolFacadeFactory}" />, использующую
        ///     <typeparamref name="TBurningToolFacadeFactory" /> для прошивания образов с именем по-умолчанию</summary>
        /// <typeparam name="TImage">Тип образа</typeparam>
        /// <typeparam name="TBurningToolFacadeFactory">Тип фабрики инструментов для прошивания образа</typeparam>
        /// <param name="Container">Контейнер, в котором будет зарегистрирована фабрика</param>
        public static IUnityContainer RegisterBurningReceiptFactory<TImage, TBurningToolFacadeFactory>(this IUnityContainer Container)
            where TImage : IImage
            where TBurningToolFacadeFactory : class, IBurningToolFacadeFactory<TImage>
        {
            return RegisterBurningReceiptFactory<TImage, TBurningToolFacadeFactory>(Container, typeof (TBurningToolFacadeFactory).FullName);
        }

        /// <summary>Регистрирует в указанном контейнере фабрику рецептов типа
        ///     <see cref="BurningReceiptFactory{TImage,TBurningToolFacadeFactory}" />, использующую
        ///     <typeparamref name="TBurningToolFacadeFactory" /> для прошивания образов</summary>
        /// <typeparam name="TImage">Тип образа</typeparam>
        /// <typeparam name="TBurningToolFacadeFactory">Тип фабрики инструментов для прошивания образа</typeparam>
        /// <param name="Container">Контейнер, в котором будет зарегистрирована фабрика</param>
        /// <param name="Name">Имя, с которым будет зарегистрирована фабрика</param>
        public static IUnityContainer RegisterBurningReceiptFactory<TImage, TBurningToolFacadeFactory>(this IUnityContainer Container, String Name)
            where TImage : IImage
            where TBurningToolFacadeFactory : class, IBurningToolFacadeFactory<TImage>
        {
            return Container.RegisterType<IBurningReceiptFactory,
                BurningReceiptFactory<TImage, TBurningToolFacadeFactory>>(Name, new ContainerControlledLifetimeManager());
        }
    }
}