using System;

namespace Core.Model
{
	public struct ContextLoaderDataValidation
	{
		public ContextLoaderDataValidation(bool value, string message)
		{
			
			Value = value;
			Message = message;

			if (!Value && string.IsNullOrEmpty(message))
				throw new ArgumentException("Message needs ot be defined is Validation is false");

		}

		public bool Value { get; }
		public string Message { get; }
	}

	public struct LoaderDataValidation<T>
	{
		public LoaderDataValidation(T item, bool value,string message)
		{
			Item = item;
			Value = value;
			Message = message;

			if(!Value && string.IsNullOrEmpty(message))
				throw new ArgumentException("Message needs ot be defined is Validation is false");

		}

		public T Item { get; }
		public bool Value { get;  }
		public string Message { get; }
	}
}
