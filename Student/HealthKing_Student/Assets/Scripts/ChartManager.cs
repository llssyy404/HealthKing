using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CP.ProChart;

public class ChartManager : MonoBehaviour
{
    //bar chart datas
    public BarChart _barChart;

    //line chart datas
    public LineChart _lineChart;

    //normalDistribution chart datas
    public LineChart _normalDistChart;

    //labels
    public GameObject _labelBar;
    public GameObject _labelLine;
    public GameObject _axisXLabel;
    public GameObject _axisYLabel;

    //2D Data set
    private ChartData2D _dataSet;
    private ChartData2D _dataSetLine;

    //2D Data set
    private ChartData2D _nomalDataSet;

    private List<Text> _barLabels;
    private List<Text> _barXLabels;
    private List<Text> _barYLabels;
    private List<Text> _lineLabels;
    private List<Text> _lineXLabels;
    private List<Text> _lineYLabels;

    private void Awake()
    {
        _barLabels = new List<Text>();
        _barXLabels = new List<Text>();
        _barYLabels = new List<Text>();
        _lineLabels = new List<Text>();
        _lineXLabels = new List<Text>();
        _lineYLabels = new List<Text>();
    }
    ///<summary>
    /// Initialize data set and charts
    ///</summary>
    void OnEnable()
    {
        _lineChart.Thickness = 0.01f;
        _lineChart.PointSize = 0.02f;
        _normalDistChart.Thickness = 0.01f;
        _normalDistChart.PointSize = 0.02f;
        _normalDistChart.Chart = LineChart.ChartType.CURVE;
        _normalDistChart.Point = LineChart.PointType.NONE;

        _dataSet = new ChartData2D();
        _dataSetLine = new ChartData2D();
        _dataSet[0, 0] = _dataSetLine[0, 0] = 10;
        _dataSet[0, 1] = _dataSetLine[0, 1] = 20;
        _dataSet[0, 2] = _dataSetLine[0, 2] = 30;
        _dataSet[0, 3] = _dataSetLine[0, 3] = 40;
        _dataSet[0, 4] = _dataSetLine[0, 4] = 50;
        //_dataSet[0, 5] = 60;
        //_dataSet[0, 6] = 50;
        //_dataSet[0, 7] = 40;
        _dataSet[1, 0] = _dataSetLine[1, 0] = 40;
        _dataSet[1, 1] = _dataSetLine[1, 1] = 25;
        _dataSet[1, 2] = _dataSetLine[1, 2] = 53;
        _dataSet[1, 3] = _dataSetLine[1, 3] = 12;
        _dataSet[1, 4] = _dataSetLine[1, 4] = 37;
        //_dataSet[1, 5] = 58;
        //_dataSet[1, 6] = 50;
        //_dataSet[1, 7] = 42;
        _barChart.SetValues(ref _dataSet);
        _lineChart.SetValues(ref _dataSetLine);

        _nomalDataSet = new ChartData2D();
        _nomalDataSet[0, 0] = 5;
        _nomalDataSet[0, 1] = 10;
        _nomalDataSet[0, 2] = 35;
        _nomalDataSet[0, 3] = 75;
        _nomalDataSet[0, 4] = 35;
        _nomalDataSet[0, 5] = 10;
        _nomalDataSet[0, 6] = 5;
        _normalDistChart.SetValues(ref _nomalDataSet);

        _labelBar.SetActive(false);
        _labelLine.SetActive(false);
        _axisXLabel.SetActive(false);
        _axisYLabel.SetActive(false);

        _barLabels.Clear();
        _lineLabels.Clear();

        for (int i = 0; i < _dataSet.Rows; i++)
        {
            for (int j = 0; j < _dataSet.Columns; j++)
            {
                GameObject obj = Instantiate(_labelBar);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_barChart.transform, false);
                Text t = obj.GetComponentInChildren<Text>();
                _barLabels.Add(t);

            }
        }

        for (int i = 0; i < _dataSetLine.Rows; i++)
        {
            for (int j = 0; j < _dataSetLine.Columns; j++)
            {
                GameObject obj = Instantiate(_labelLine);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_lineChart.transform, false);
                Text t = obj.GetComponent<Text>();
                _lineLabels.Add(t);
            }
        }

        _barXLabels.Clear();
        _lineXLabels.Clear();

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = Instantiate(_axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = (i + 1) + "바퀴";
            _barXLabels.Add(t);
        }

        for (int i = 0; i < _dataSetLine.Columns; i++)
        {
            GameObject obj = Instantiate(_axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_lineChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = (i + 1) + "바퀴";
            _lineXLabels.Add(t);
        }

        _barYLabels.Clear();
        _lineYLabels.Clear();

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _barYLabels.Add(t);
        }

        for (int i = 0; i < _dataSetLine.Columns; i++)
        {
            GameObject obj = Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_lineChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _lineYLabels.Add(t);
        }
    }

    ///<summary>
    /// Remove hanlders when object is disabled
    ///</summary>
    void OnDisable()
    {

    }

    void Update()
    {
        UpdateLabels();
    }

    ///<summary>
    /// Update labels
    ///</summary>
    public void UpdateLabels()
    {
        for (int i = 0; i < _dataSet.Rows; i++)
        {
            for (int j = 0; j < _dataSet.Columns; j++)
            {
                LabelPosition labelPos = _barChart.GetLabelPosition(i, j, 1.0f);
                if (labelPos != null)
                {
                    _barLabels[i * _dataSet.Columns + j].transform.parent.gameObject.SetActive(true);
                    _barLabels[i * _dataSet.Columns + j].text = labelPos.value.ToString("0.00");
                    _barLabels[i * _dataSet.Columns + j].transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition = labelPos.position;
                }
            }
        }

        for (int i = 0; i < _dataSetLine.Rows; i++)
        {
            for (int j = 0; j < _dataSetLine.Columns; j++)
            {
                LabelPosition labelPos = _lineChart.GetLabelPosition(i, j);
                if (labelPos != null)
                {
                    _lineLabels[i * _dataSetLine.Columns + j].gameObject.SetActive(true);
                    _lineLabels[i * _dataSetLine.Columns + j].text = labelPos.value.ToString("0.00");
                    _lineLabels[i * _dataSetLine.Columns + j].rectTransform.anchoredPosition = labelPos.position;
                }
            }
        }

        LabelPosition[] positions = _barChart.GetAxisXPositions();
        if (positions != null)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                _barXLabels[i].gameObject.SetActive(true);
                _barXLabels[i].GetComponent<RectTransform>().anchoredPosition = positions[i].position;
            }
        }

        positions = _barChart.GetAxisYPositions();
        if (positions != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (positions.Length - 1 < i)
                {
                    _barYLabels[i].gameObject.SetActive(false);
                }
                else
                {
                    _barYLabels[i].gameObject.SetActive(true);
                    _barYLabels[i].text = positions[i].value.ToString("0.0");
                    _barYLabels[i].GetComponent<RectTransform>().anchoredPosition = positions[i].position;
                }
            }
        }

        positions = _lineChart.GetAxisXPositions();
        if (positions != null)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                _lineXLabels[i].gameObject.SetActive(true);
                _lineXLabels[i].GetComponent<RectTransform>().anchoredPosition = positions[i].position;
            }
        }

        positions = _lineChart.GetAxisYPositions();
        if (positions != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (positions.Length - 1 < i)
                {
                    _lineYLabels[i].gameObject.SetActive(false);
                }
                else
                {
                    _lineYLabels[i].gameObject.SetActive(true);
                    _lineYLabels[i].text = positions[i].value.ToString("0.0");
                    _lineYLabels[i].GetComponent<RectTransform>().anchoredPosition = positions[i].position;
                }
            }
        }
    }

    public void SetCardiTrackRecordBarAndLineGraph()
    {
        List<TrackRecord> trackRecord = DataManager.GetInstance().trackRecordList;
        List<int> avgTrackRecordList = DataManager.GetInstance().avgTrackRecordList;
        if (trackRecord.Count != avgTrackRecordList.Count)
        {
            Debug.Log("trackRecord.Count != dicAvgTrackRecord.Count");
            return;
        }

        _dataSet.Clear();
        _dataSetLine.Clear();
        _dataSet = new ChartData2D();
        _dataSetLine = new ChartData2D();
        for (int i = 0; i < trackRecord.Count; ++i)
        {
            _dataSet[0, i] = _dataSetLine[0, i] = trackRecord[i].elapsedTime;
            _dataSet[1, i] = _dataSetLine[1, i] = avgTrackRecordList[i];
        }
        _barChart.SetValues(ref _dataSet);
        _lineChart.SetValues(ref _dataSetLine);

        for (int i = 0; i < _barLabels.Count; ++i)
        {
            Destroy(_barLabels[i].rectTransform.parent.gameObject);
        }
        _barLabels.Clear();
        for (int i = 0; i < _lineLabels.Count; ++i)
        {
            Destroy(_lineLabels[i].gameObject);
        }
        _lineLabels.Clear();
        for (int i = 0; i < _barXLabels.Count; ++i)
        {
            Destroy(_barXLabels[i].gameObject);
        }
        _barXLabels.Clear();
        for (int i = 0; i < _lineXLabels.Count; ++i)
        {
            Destroy(_lineXLabels[i].gameObject);
        }
        _lineXLabels.Clear();
        for (int i = 0; i < _barYLabels.Count; ++i)
        {
            Destroy(_barYLabels[i].gameObject);
        }
        _barYLabels.Clear();
        for (int i = 0; i < _lineYLabels.Count; ++i)
        {
            Destroy(_lineYLabels[i].gameObject);
        }
        _lineYLabels.Clear();

        for (int i = 0; i < _dataSet.Rows; i++)
        {
            for (int j = 0; j < _dataSet.Columns; j++)
            {
                GameObject obj = Instantiate(_labelBar);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_barChart.transform, false);
                Text t = obj.GetComponentInChildren<Text>();
                _barLabels.Add(t);
            }
        }

        for (int i = 0; i < _dataSetLine.Rows; i++)
        {
            for (int j = 0; j < _dataSetLine.Columns; j++)
            {
                GameObject obj = Instantiate(_labelLine);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_lineChart.transform, false);
                Text t = obj.GetComponent<Text>();
                _lineLabels.Add(t);
            }
        }

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = Instantiate(_axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = (i + 1) + "바퀴";
            _barXLabels.Add(t);
        }

        for (int i = 0; i < _dataSetLine.Columns; i++)
        {
            GameObject obj = Instantiate(_axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_lineChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = (i + 1) + "바퀴";
            _lineXLabels.Add(t);
        }

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _barYLabels.Add(t);
        }

        for (int i = 0; i < _dataSetLine.Columns; i++)
        {
            GameObject obj = Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_lineChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _lineYLabels.Add(t);
        }
    }

    public void SetAgileRecordBarGraph(int elapsedTime)
    {
        int avgElapsedTime = DataManager.GetInstance().avgAgileRecord;
        _dataSet.Clear();
        _dataSet = new ChartData2D();
        _dataSet[0, 0] = elapsedTime;
        _dataSet[1, 0] = avgElapsedTime;
        _barChart.SetValues(ref _dataSet);

        for (int i = 0; i < _barLabels.Count; ++i)
        {
            Destroy(_barLabels[i].rectTransform.parent.gameObject);
        }
        _barLabels.Clear();
        for (int i = 0; i < _barXLabels.Count; ++i)
        {
            Destroy(_barXLabels[i].gameObject);
        }
        _barXLabels.Clear();
        for (int i = 0; i < _barYLabels.Count; ++i)
        {
            Destroy(_barYLabels[i].gameObject);
        }
        _barYLabels.Clear();

        for (int i = 0; i < _dataSet.Rows; i++)
        {
            for (int j = 0; j < _dataSet.Columns; j++)
            {
                GameObject obj = Instantiate(_labelBar);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_barChart.transform, false);
                Text t = obj.GetComponentInChildren<Text>();
                _barLabels.Add(t);
            }
        }

        //for (int i = 0; i < _dataSet.Columns; i++)
        //{
        //    GameObject obj = Instantiate(_axisXLabel);
        //    obj.name = "Label" + i;
        //    obj.transform.SetParent(_barChart.transform, false);
        //    Text t = obj.GetComponent<Text>();
        //    t.text = (i + 1) + "바퀴";
        //    _barXLabels.Add(t);
        //}

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _barYLabels.Add(t);
        }
    }

    public void SetMuscRecordBarGraph(int count)
    {
        int avgCount = DataManager.GetInstance().avgMuscRecord;
        _dataSet.Clear();
        _dataSet = new ChartData2D();
        _dataSet[0, 0] = count;
        _dataSet[1, 0] = avgCount;
        _barChart.SetValues(ref _dataSet);

        for (int i = 0; i < _barLabels.Count; ++i)
        {
            Destroy(_barLabels[i].rectTransform.parent.gameObject);
        }
        _barLabels.Clear();
        for (int i = 0; i < _barXLabels.Count; ++i)
        {
            Destroy(_barXLabels[i].gameObject);
        }
        _barXLabels.Clear();
        for (int i = 0; i < _barYLabels.Count; ++i)
        {
            Destroy(_barYLabels[i].gameObject);
        }
        _barYLabels.Clear();

        for (int i = 0; i < _dataSet.Rows; i++)
        {
            for (int j = 0; j < _dataSet.Columns; j++)
            {
                GameObject obj = Instantiate(_labelBar);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_barChart.transform, false);
                Text t = obj.GetComponentInChildren<Text>();
                _barLabels.Add(t);
            }
        }

        //for (int i = 0; i < _dataSet.Columns; i++)
        //{
        //    GameObject obj = Instantiate(_axisXLabel);
        //    obj.name = "Label" + i;
        //    obj.transform.SetParent(_barChart.transform, false);
        //    Text t = obj.GetComponent<Text>();
        //    t.text = (i + 1) + "바퀴";
        //    _barXLabels.Add(t);
        //}

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _barYLabels.Add(t);
        }
    }

}
