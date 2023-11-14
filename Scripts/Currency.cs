using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
public class NewBehaviourScript : MonoBehaviour
{
	public AudioClip goodSpeak;
	public AudioClip normalSpeak;
	public AudioClip badSpeak;
	private AudioSource selectAudio;
	private Dictionary<string,float> dataSet = new Dictionary<string, float>();
	private bool statusStart = false;
	private int i = 1;
	
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(GoogleSheets());
	}
// Update is called once per frame
	void Update()
	{
		if (dataSet["Mon_" + i.ToString()] > 100 & statusStart == false & i != dataSet.Count)
		{
			StartCoroutine(PlaySelectAudioGood());
			Debug.Log(dataSet["Mon_" + i.ToString()]);
		}
		if (dataSet["Mon_" + i.ToString()] >= 60 & dataSet["Mon_" + i.ToString()] < 100 & statusStart == false & i != dataSet.Count)
		{
			StartCoroutine(PlaySelectAudioNormal());
			Debug.Log(dataSet["Mon_" + i.ToString()]);
		}
		if (dataSet["Mon_" + i.ToString()] < 60 & statusStart == false & i != dataSet.Count)
		{
			StartCoroutine(PlaySelectAudioBad());
			Debug.Log(dataSet["Mon_" + i.ToString()]);
		}
	}
	
	IEnumerator GoogleSheets()
	{
		UnityWebRequest curentResp =
		UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1PIxbVUFUv8tmFCOvGs9nJr0tj2tS_B5ZmPs34BgcTeM/values/Лист1?key=AIzaSyAfWm8V9ybLPx7ne9A4xlGs8lPrwUW6CN4");
		yield return curentResp.SendWebRequest();
		string rawResp = curentResp.downloadHandler.text;
		var rawJson = JSON.Parse(rawResp);
		foreach (var itemRawJson in rawJson["values"])
		{
			var parseJson = JSON.Parse(itemRawJson.ToString());
			var selectRow = parseJson[0].AsStringList;
			dataSet.Add(("Mon_" + selectRow[0]),
			float.Parse(selectRow[1]));
		}
	}

	IEnumerator PlaySelectAudioGood()
	{
		statusStart = true;
		selectAudio = GetComponent<AudioSource>();
		selectAudio.clip = goodSpeak;
		selectAudio.Play();
		yield return new WaitForSeconds(3);
		statusStart = false;
		i++;
	}

	IEnumerator PlaySelectAudioNormal()
	{
		statusStart = true;
		selectAudio = GetComponent<AudioSource>();
		selectAudio.clip = normalSpeak;
		selectAudio.Play();
		yield return new WaitForSeconds(3);
		statusStart = false;
		i++;
	}

	IEnumerator PlaySelectAudioBad()
	{
		statusStart = true;
		selectAudio = GetComponent<AudioSource>();
		selectAudio.clip = badSpeak;
		selectAudio.Play();
		yield return new WaitForSeconds(4);
		statusStart = false;
		i++;
	}
}