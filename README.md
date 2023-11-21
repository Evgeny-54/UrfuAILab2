# АНАЛИЗ ДАННЫХ И ИСКУССТВЕННЫЙ ИНТЕЛЛЕКТ [in GameDev]
Отчет по лабораторной работе #2 выполнил:
- Жевакин Евгений Дмитриевич
- РИ221120
Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
-  Выберите одну из компьютерных игр, приведите скриншот её
геймплея и краткое описание концепта игры. Выберите одну из игровых переменных
в игре (ресурсы, внутри игровая валюта, здоровье персонажей и т.д.), опишите её
роль в игре, условия изменения / появления и диапазон допустимых значений.
Постройте схему экономической модели в игре и укажите место выбранного ресурса
в ней.
- Задание 2.
-  С помощью скрипта на языке Python заполните google-таблицу
данными, описывающими выбранную игровую переменную в выбранной игре (в
качестве таких переменных может выступать игровая валюта, ресурсы, здоровье и
т.д.). Средствами google-sheets визуализируйте данные в google-таблице (постройте
график, диаграмму и пр.) для наглядного представления выбранной игровой
величины.
- Задание 3
- Настройте на сцене Unity воспроизведение звуковых файлов,
описывающих динамику изменения выбранной переменной. Например, если
выбрано здоровье главного персонажа вы можете выводить сообщения, связанные с
его состоянием.
- Выводы.
- ✨Magic ✨

## Цель работы
Научиться передавать в Unity данные из Google Sheets с
помощью Python.

## Задание 1
Выберите одну из компьютерных игр, приведите скриншот её геймплея и краткое описание концепта игры. Выберите одну из игровых переменных
в игре (ресурсы, внутри игровая валюта, здоровье персонажей и т.д.), опишите её
роль в игре, условия изменения / появления и диапазон допустимых значений.
Постройте схему экономической модели в игре и укажите место выбранного ресурса
в ней.
### Решение задачи 1
#### Anno 1404
Для примера мы выберем игру Anno 1404.

Как и в Anno 1701, игровой мир разделен на две различающиеся культурой зоны. В Anno 1404 эти зоны были вдохновлены, хоть и без претензии на историческую точность, северо-западной Европой эпохи Ренессанса и средневековым Ближним Востоком, упоминаемыми как Европа и Восток соответственно. Важнейшим отличием от предыдущих частей серии является необходимость строительства Европейских и Восточных поселений одновременно с целью обеспечения прогресса населения и расширения строительных возможностей. Однако несмотря на возможность напрямую управлять созданием и развитием Восточных поселений, фокус игры по прежнему приходится на Европейские поселения. Восток остается торговым партнёром Европы, позволяя её населению повышать класс. Для этого Восточные товары, такие как специи, индиго и кварц, должны быть произведены и доставлены из Восточных колоний.
<picture>

 <img alt="Example_game.jpg" src="https://github.com/Evgeny-54/UrfuAILab2/blob/main/Example_game.jpg">
</picture>

#### Ресурс из Anno 1404
Для выполнения нашей задачи мы возьмем ресурс "cпеции", который вырабатывается при своевременном поливе земли на Восточной колонии. Используется спросом у горожан. Возможна продажа и покупка у других игроков. Максимальная производительность плантации 300 единиц.

<picture>
 <img alt="spices.jpg" src="https://github.com/Evgeny-54/UrfuAILab2/blob/main/spices.jpg">
</picture>


Схема ресурса

<picture>

 <img alt="shema.jpg" src="https://github.com/Evgeny-54/UrfuAILab2/blob/main/shema.jpg">
</picture>







## Задание 2
С помощью скрипта на языке Python заполните google-таблицу данными, описывающими выбранную игровую переменную в выбранной игре (в качестве таких переменных может выступать игровая валюта, ресурсы, здоровье и т.д.). Средствами google-sheets визуализируйте данные в google-таблице (постройте график, диаграмму и пр.) для наглядного представления выбранной игровой величины.

### Решение задачи 2
Производительность плантации от влажности почвы
<picture>

 <img alt="image.png" src="https://github.com/Evgeny-54/UrfuAILab2/blob/main/image.png">
</picture>

key.json - файл с ключом для api

```python

import gspread
import numpy as np
gc = gspread.service_account(filename='key.json')
sh = gc.open("unityAI2")
#Максимальная выработка плантации
price = 300
#Влажность почвы
water = 0.99
mon = list(range(1,23))
i = 0
while i <= len(mon):
    i += 1
    if i == 0:
        continue
    else:
        t = np.random.randint(5,9) / 100
        #Если земля сухая, то полить
        if water-t <= 0:
            water = 0.99
        #Высыхание почвы
        water-=t
        #Выработка
        product = round(price*water)
        tempInf = round(water, 2)
        tempInf = str(tempInf)
        tempInf = tempInf.replace('.',',')
        sh.sheet1.update(('A' + str(i)), str(i))
        sh.sheet1.update(('B' + str(i)), product)
        sh.sheet1.update(('C' + str(i)), str(tempInf))
        
        print(tempInf)

```
## Задание 3
Настройте на сцене Unity воспроизведение звуковых файлов,
описывающих динамику изменения выбранной переменной. Например, если
выбрано здоровье главного персонажа вы можете выводить сообщения, связанные с
его состоянием.
### Решение задачи 3

Воспроизводятся звуковые файлы, в зависимости от производительности плантации


<picture>

 <img alt="unity.png" src="https://github.com/Evgeny-54/UrfuAILab2/blob/main/unity.png">
</picture>

Для работы скрипта следует скачать звуковые файлы из папки Sound

API_KEY - ключ api

```c#
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
		// Если производительность больше 100
		if (dataSet["Mon_" + i.ToString()] > 100 & statusStart == false & i != dataSet.Count)
		{
			StartCoroutine(PlaySelectAudioGood());
			Debug.Log(dataSet["Mon_" + i.ToString()]);
		}
		// Если производительность больше или равно 60
		if (dataSet["Mon_" + i.ToString()] >= 60 & dataSet["Mon_" + i.ToString()] < 100 & statusStart == false & i != dataSet.Count)
		{
			StartCoroutine(PlaySelectAudioNormal());
			Debug.Log(dataSet["Mon_" + i.ToString()]);
		}
		// Если производительность меньше 60
		if (dataSet["Mon_" + i.ToString()] < 60 & statusStart == false & i != dataSet.Count)
		{
			StartCoroutine(PlaySelectAudioBad());
			Debug.Log(dataSet["Mon_" + i.ToString()]);
		}
	}
	
	IEnumerator GoogleSheets()
	{
		UnityWebRequest curentResp =
		UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1PIxbVUFUv8tmFCOvGs9nJr0tj2tS_B5ZmPs34BgcTeM/values/Лист1?key=API_KEY");
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

```

## Выводы

В ходе выполнения данной лабароторной работы, я узнал как работать с api google sheets и научился работать с ним в Unity


