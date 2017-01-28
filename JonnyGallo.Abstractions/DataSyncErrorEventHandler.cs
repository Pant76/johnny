﻿using System;
namespace JonnyGallo.Abstractions
{
	/// <summary>
	/// A generically-typed delegate for handling data sync errors.
	/// </summary>
	public delegate void DataSyncErrorEventHandler<T>(object sender, DataSyncErrorEventArgs<T> e);
}

