//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC.Unity.InterceptionBehaviors
{
    /// <summary>
    /// Audit behavior
    /// </summary>
    class AuditBehavior : IInterceptionBehavior, IDisposable
    {
        #region Members

        private TraceSource source;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of Audit Behavior
        /// </summary>
        /// <param name="source">The trace source to flush audit information</param>
        public AuditBehavior(TraceSource source)
        {
            if (source == null) throw new ArgumentNullException("source");

            this.source = source;
        }

        #endregion

        #region IInterceptionBehavior implementation

        /// <summary>
        /// <see cref="Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior"/></returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        /// <summary>
        /// <see cref="Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior"/>
        /// </summary>
        /// <param name="input"><see cref="Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior"/></param>
        /// <param name="getNext"><see cref="Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior"/></param>
        /// <returns><see cref="Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior"/></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            this.source.TraceInformation(
                "Invoking {0}",
                input.MethodBase.ToString());

            string methodName = input.MethodBase.Name;
            decimal moneyAmountToTransfer = (decimal)input.Arguments[2];

            if (methodName == "PerformTransfer" && moneyAmountToTransfer > 10)
            {
                this.source.TraceInformation(
                "***** Atención, cantidad umbral superada. Se están transfiriendo {0} €  *****",
                moneyAmountToTransfer.ToString());
            }


            IMethodReturn methodReturn = getNext().Invoke(input, getNext);

            if (methodReturn.Exception == null)
            {
                this.source.TraceInformation(
                    "Successfully finished {0}",
                    input.MethodBase.ToString());
            }
            else
            {
                this.source.TraceInformation(
                    "Finished {0} with exception {1}: {2}",
                    input.MethodBase.ToString(),
                    methodReturn.Exception.GetType().Name,
                    methodReturn.Exception.Message);
            }

            this.source.Flush();

            return methodReturn;
        }

        /// <summary>
        /// <see cref="Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior"/>
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }
        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (source != null)
            {
                source.Flush();
                source.Close();

                source = null;
            }

        }

        #endregion
    }
}


