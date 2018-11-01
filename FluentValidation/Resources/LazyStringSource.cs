﻿#region License
// Copyright (c) Jeremy Skinner (http://www.jeremyskinner.co.uk)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fluentvalidation.codeplex.com
#endregion
namespace FluentValidation.Resources {
	using System;

	/// <summary>
	/// Lazily loads the string
	/// </summary>
	public class LazyStringSource : IStringSource {
		readonly Func<string> _stringProvider;

		/// <summary>
		/// Creates a new LazyStringSource
		/// </summary>
		/// <param name="stringProvider"></param>
		public LazyStringSource(Func<string> stringProvider) {
			_stringProvider = stringProvider;
		}

		/// <summary>
		/// Gets the value
		/// </summary>
		/// <returns></returns>
		public string GetString() {
			return _stringProvider();
		}

		/// <summary>
		/// Resource type
		/// </summary>
		public string ResourceName { get { return null; } }

		/// <summary>
		/// Resource name
		/// </summary>
		public Type ResourceType { get { return null; } }
	}
}