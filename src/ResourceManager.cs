#region License
//
// The Open Toolkit Library License
//
// Copyright (c) 2006 - 2009 the Open Toolkit library.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights to 
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using HaighFramework;

namespace BearsEngine
{
    public sealed class ResourceManager : IDisposable
    {
        private static readonly object _syncRoot = new object();
        private static readonly ResourceManager default_implementation = new ResourceManager();
        public static ResourceManager Default => default_implementation;

        readonly List<IDisposable> Resources = new List<IDisposable>();
        private bool _disposed;

        public void RegisterResource(IDisposable resource)
        {
            lock (_syncRoot)
            {
                Resources.Add(resource);
            }
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ResourceManager()
        {
            Dispose(false);
        }

        private void Dispose(bool manual)
        {
            if (!_disposed)
            {
                if (manual)
                {
                    lock (_syncRoot)
                    {
                        foreach (var resource in Resources)
                            resource.Dispose();

                        Resources.Clear();
                    }
                }
                else
                {
                    HConsole.Log("{0} leaked with {1} live resources, did you forget to call Dispose()?", GetType().FullName, Resources.Count);
                }
                _disposed = true;
            }
        }
        #endregion
    }
}
