﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Thinktecture.IdentityServer.Core.Models;
namespace Thinktecture.IdentityServer.Core.Events
{
    /// <summary>
    /// Event class for logout events
    /// </summary>
    public class LogoutEvent : EventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutEvent"/> class.
        /// </summary>
        public LogoutEvent()
            : base(EventConstants.Categories.Authentication)
        {
            Id = EventConstants.Ids.Logout;
            EventType = EventType.Success;
            Message = Resources.Events.LogoutEvent;
        }

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>
        /// The subject identifier.
        /// </value>
        public string SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sign out message.
        /// </summary>
        /// <value>
        /// The sign out message.
        /// </value>
        public SignOutMessage SignOutMessage { get; set; }
    }
}