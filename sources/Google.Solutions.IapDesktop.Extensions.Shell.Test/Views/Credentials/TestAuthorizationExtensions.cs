﻿//
// Copyright 2020 Google LLC
//
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//

using Google.Solutions.Common.Test;
using Google.Solutions.IapDesktop.Application.Services.Authorization;
using Google.Solutions.IapDesktop.Extensions.Shell.Views.Credentials;
using Moq;
using NUnit.Framework;
using System;

namespace Google.Solutions.IapDesktop.Extensions.Shell.Test.Views.Credentials
{
    [TestFixture]
    public class TestAuthorizationExtensions : CommonFixtureBase
    {
        [Test]
        public void WhenAuthorizationEmailIsNull_ThenSuggestWindowsUsernameReturnsWindowsUsername()
        {
            var authorization = new Mock<IAuthorization>();
            authorization.SetupGet(a => a.Email).Returns((string)null);

            var suggestedUsername = authorization.Object.SuggestWindowsUsername();

            Assert.AreEqual(Environment.UserName, suggestedUsername);
        }

        [Test]
        public void WhenAuthorizationHasInvalidEmail_ThenSuggestWindowsUsernameReturnsWindowsUsername()
        {
            var authorization = new Mock<IAuthorization>();
            authorization.SetupGet(a => a.Email).Returns("joe");

            var suggestedUsername = authorization.Object.SuggestWindowsUsername();

            Assert.AreEqual(Environment.UserName, suggestedUsername);
        }

        [Test]
        public void WhenAuthorizationHasOverlyLongEmail_ThenSuggestWindowsUsernameReturnsUserPartOfEmail()
        {
            var authorization = new Mock<IAuthorization>();
            authorization.SetupGet(a => a.Email).Returns("abcde12345abcde12345abcde12345@example.com");

            var suggestedUsername = authorization.Object.SuggestWindowsUsername();

            Assert.AreEqual("abcde12345abcde12345", suggestedUsername);
        }

        [Test]
        public void WhenAuthorizationHasValidEmail_ThenSuggestWindowsUsernameReturnsUserPartOfEmail()
        {
            var authorization = new Mock<IAuthorization>();
            authorization.SetupGet(a => a.Email).Returns("joe@example.com");

            var suggestedUsername = authorization.Object.SuggestWindowsUsername();

            Assert.AreEqual("joe", suggestedUsername);
        }
    }
}
