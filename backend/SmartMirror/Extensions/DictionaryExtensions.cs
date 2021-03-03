namespace SmartMirror.Extensions
{
	using System.Collections.Generic;

	public static class DictionaryExtensions
	{
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
			=> dictionary.TryGetValue(key, out var v) ? v : default;
	}
}
