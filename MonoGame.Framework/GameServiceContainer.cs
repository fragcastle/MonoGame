#region License
/*
MIT License
Copyright � 2006 The Mono.Xna Team

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

using System;
#if WINRT
using System.Reflection;
#endif
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Microsoft.Xna.Framework
{
    public class GameServiceContainer : IServiceProvider, IServiceContainer
    {
        Dictionary<Type, object> services;

        public GameServiceContainer()
        {
            services = new Dictionary<Type, object>();
        }

        public void AddService(Type type, object provider)
        {
            AddService(type, provider, false);
        }

        public void AddService( Type type, object provider, bool promote )
        {
            if( type == null )
                throw new ArgumentNullException( "type" );
            if( provider == null )
                throw new ArgumentNullException( "provider" );
#if WINRT
            if (!type.GetTypeInfo().IsAssignableFrom(provider.GetType().GetTypeInfo()))
#else
            if( !type.IsAssignableFrom( provider.GetType() ) )
#endif
                throw new ArgumentException( "The provider does not match the specified service type!" );

            services.Add( type, provider );
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            throw new NotImplementedException();
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            throw new NotImplementedException();
        }

        public object GetService(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            object service;
            if (services.TryGetValue(type, out service))
                return service;

            return null;
        }

        public void RemoveService(Type type)
        {
            RemoveService(type, false);
        }

        public void RemoveService(Type type, bool promote)
        {
            if( type == null )
                throw new ArgumentNullException( "type" );

            services.Remove( type );
        }
    }
}