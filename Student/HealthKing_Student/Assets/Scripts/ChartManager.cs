using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CP.ProChart;

public class ChartManager : MonoBehaviour {

    static private ChartManager _instance;
    static public ChartManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ChartManager();
        }
        return _instance;
    }

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
    private ChartData2D _dataSet = new ChartData2D();
    public ChartData2D dataSet
    {
        get { return _dataSet; }
        set { _dataSet = value; }
    }

    //2D Data set
    private ChartData2D _nomalDataSet;

    private List<Text> _barLabels = new List<Text>();
    private List<Text> _barXLabels = new List<Text>();
    private List<Text> _barYLabels = new List<Text>();
    private List<Text> _lineLabels = new List<Text>();
    private List<Text> _lineXLabels = new List<Text>();
    private List<Text> _lineYLabels = new List<Text>();

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

        //_dataSet = new ChartData2D();
        _dataSet[0, 0] = 10;
        _dataSet[0, 1] = 20;
        _dataSet[0, 2] = 30;
        _dataSet[0, 3] = 40;
        _dataSet[0, 4] = 50;
        _dataSet[0, 5] = 60;
        _dataSet[0, 6] = 50;
        _dataSet[0, 7] = 40;
        _dataSet[1, 0] = 40;
        _dataSet[1, 1] = 25;
        _dataSet[1, 2] = 53;
        _dataSet[1, 3] = 12;
        _dataSet[1, 4] = 37;
        _dataSet[1, 5] = 58;
        _dataSet[1, 6] = 50;
        _dataSet[1, 7] = 42;
        _barChart.SetValues(ref _dataSet);
        _lineChart.SetValues(ref _dataSet);

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

        for (int i = 0; i < dataSet.Rows; i++)
        {
            for (int j = 0; j < dataSet.Columns; j++)
            {
                GameObject obj = (GameObject)Instantiate(_labelBar);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_barChart.transform, false);
                Text t = obj.GetComponentInChildren<Text>();
                _barLabels.Add(t);

                obj = (GameObject)Instantiate(_labelLine);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_lineChart.transform, false);
                t = obj.GetComponent<Text>();
                _lineLabels.Add(t);
            }
        }

        _barXLabels.Clear();
        _lineXLabels.Clear();

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = (GameObject)Instantiate(_axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = (i+1) + "바퀴";
            _barXLabels.Add(t);

            obj = (GameObject)Instantiate(_axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_lineChart.transform, false);
            t = obj.GetComponent<Text>();
            t.text = (i + 1) + "바퀴";
            _lineXLabels.Add(t);
        }

        _barYLabels.Clear();
        _lineYLabels.Clear();

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = (GameObject)Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _barYLabels.Add(t);

            obj = (GameObject)Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_lineChart.transform, false);
            t = obj.GetComponent<Text>();
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

                labelPos = _lineChart.GetLabelPosition(i, j);
                if (labelPos != null)
                {
                    _lineLabels[i * _dataSet.Columns + j].gameObject.SetActive(true);
                    _lineLabels[i * _dataSet.Columns + j].text = labelPos.value.ToString("0.00");
                    _lineLabels[i * _dataSet.Columns + j].rectTransform.anchoredPosition = labelPos.position;
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
    
    public void SetA()
    {
        List<TrackRecord> trackRecord = DataManager.GetInstance().trackRecordList;
        _dataSet.Resize(1, trackRecord.Count);
        for (int i = 0; i < trackRecord.Count; ++i)
        {
            _dataSet[0, i] = trackRecord[i].elapsedTime;
            _dataSet[1, i] = trackRecord[i].elapsedTime;
        }
        //_barChart.Dirty = true;
        //_barChart.SetValues(ref _dataSet);
        //_lineChart.SetValues(ref _dataSet);

        _labelBar.SetActive(false);
        _labelLine.SetActive(false);
        _axisXLabel.SetActive(false);
        _axisYLabel.SetActive(false);

        for(int i =0; i < _barLabels.Count; ++i)
        {
            Destroy(_barLabels[i]);
        }
        _barLabels.Clear();
        for (int i = 0; i < _lineLabels.Count; ++i)
        {
            Destroy(_lineLabels[i]);
        }
        _lineLabels.Clear();

        for (int i = 0; i < dataSet.Rows; i++)
        {
            for (int j = 0; j < dataSet.Columns; j++)
            {
                GameObject obj = Instantiate(_labelBar);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_barChart.transform, false);
                Text t = obj.GetComponentInChildren<Text>();
                _barLabels.Add(t);

                obj = Instantiate(_labelLine);
                obj.name = "Label" + i + "_" + j;
                obj.transform.SetParent(_lineChart.transform, false);
                t = obj.GetComponent<Text>();
                _lineLabels.Add(t);
            }
        }

        for (int i = 0; i < _barXLabels.Count; ++i)
        {
            Destroy(_barXLabels[i]);
        }
        _barXLabels.Clear();
        for (int i = 0; i < _lineXLabels.Count; ++i)
        {
            Destroy(_lineXLabels[i]);
        }
        _lineXLabels.Clear();

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = Instantiate(_axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = (i + 1) + "바퀴";
            _barXLabels.Add(t);

            obj = Instantiate(_axisXLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_lineChart.transform, false);
            t = obj.GetComponent<Text>();
            t.text = (i + 1) + "바퀴";
            _lineXLabels.Add(t);
        }

        for (int i = 0; i < _barYLabels.Count; ++i)
        {
            Destroy(_barYLabels[i]);
        }
        _barYLabels.Clear();
        for (int i = 0; i < _lineYLabels.Count; ++i)
        {
            Destroy(_lineYLabels[i]);
        }
        _lineYLabels.Clear();

        for (int i = 0; i < _dataSet.Columns; i++)
        {
            GameObject obj = Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_barChart.transform, false);
            Text t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _barYLabels.Add(t);

            obj = Instantiate(_axisYLabel);
            obj.name = "Label" + i;
            obj.transform.SetParent(_lineChart.transform, false);
            t = obj.GetComponent<Text>();
            t.text = t.gameObject.name;
            _lineYLabels.Add(t);
        }

    }
}
