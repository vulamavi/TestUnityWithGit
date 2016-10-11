using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class Package{
	int dataLength;
	char[] data;
	int messageCode;
	public Package(){
	}

	int GetDataLength() {
		return dataLength;
	}

	void SetDataLength(int dataLength) {
		this.dataLength = dataLength;
	}

	int GetMessageCode() {
		return messageCode;
	}

	void SetMessageCode(int messageCode) {
		this.messageCode = messageCode;
	}

	char[] GetData() {
		return data;
	}

	void SetData(char[] data) {
		Debug.Log ("char Data : " + data.Length);

		if (data == null) {
			data = new char[512];
			Array.Clear(data, 0, data.Length);
		}
		else {
			this.data = data;
		}

	}

	public void CreatePackage(int dataLength, int messageCode, char[] data){
		SetDataLength(dataLength);
		SetMessageCode(messageCode);
		SetData(data);
	}
}