﻿/* Copyright (c) Citrix Systems Inc. 
 * All rights reserved. 
 * 
 * Redistribution and use in source and binary forms, 
 * with or without modification, are permitted provided 
 * that the following conditions are met: 
 * 
 * *   Redistributions of source code must retain the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer. 
 * *   Redistributions in binary form must reproduce the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer in the documentation and/or other 
 *     materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
 * SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using XenAPI;

namespace XenAdmin.Wizards.GenericPages
{
    public class HomeServerItem : DelayLoadingOptionComboBoxItem
    {
        private readonly List<ReasoningFilter> filters;

        public HomeServerItem(IXenObject host, List<ReasoningFilter> filters)
            : base(host)
        {
            if(!(host is Host))
            {
                throw new ArgumentException("This class expects as IXenObject of type host");
            }

            this.filters = filters;
            LoadAndWait();
        }

        protected override string FetchFailureReason()
        {
            foreach (ReasoningFilter filter in filters)
            {
                if (filter.FailureFoundFor(Item))
                {
                    return filter.Reason;
                }
            }

            return String.Empty;
        }
    }

    public class DoNotAssignHomeServerPoolItem : IEnableableXenObjectComboBoxItem
    {
        private readonly IXenObject pool;
        public DoNotAssignHomeServerPoolItem(IXenObject pool)
        {
            this.pool = pool;
            if(!(pool is Pool))
                throw new ArgumentException("This class epects as IXenObject of type pool");
        }

        public IXenObject Item
        {
            get { return pool; }
        }

        public bool Enabled
        {
            get { return true; }
        }

        public override string ToString()
        {
            return Messages.DONT_SELECT_TARGET_SERVER;
        }
    }
}