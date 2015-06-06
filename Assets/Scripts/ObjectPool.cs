using System.Collections;
using System.Collections.Generic;


public class ObjectPool<T> : Dictionary<T, bool> {
	public ObjectPool() {
	}

	public void AddObject(T obj) {
		this.Add (obj, false);
	}

	public T RequestObject() {
		foreach (T obj in this.Keys) {
			if (!this[obj]) {
				this[obj] = true;
				return obj;
			}
		}

		return default(T);
	}

	public void ReturnObject(T obj) {
		this [obj] = false; 
	}
}
