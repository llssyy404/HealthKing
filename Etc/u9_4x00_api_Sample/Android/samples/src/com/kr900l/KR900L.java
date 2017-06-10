package com.kr900l;

//import java.math.BigDecimal;
//import java.sql.Date;
//import java.util.Date;
//import java.text.SimpleDateFormat;
//import java.util.Arrays;
//import java.util.Locale;
import java.lang.reflect.InvocationTargetException;
import java.util.Timer;
import java.util.TimerTask;

import java.util.ArrayList;

import android.app.Activity;
import android.app.ActivityManager;
import android.app.AlertDialog;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.ProgressDialog;
import android.bluetooth.BluetoothAdapter;

import android.content.BroadcastReceiver;
import android.content.DialogInterface;
import android.content.Intent;

import android.content.DialogInterface.OnClickListener;
import android.media.AudioManager;
import android.media.SoundPool;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.os.Vibrator;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;

import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;

import com.kr900l.ConnectSocket.connectBluetoothState;

public class KR900L extends Activity implements View.OnClickListener {
	public static final int MENU_ABOUT_ID = 101;
	public static final int MENU_SETTING_ID = 102;
	public static final int MENU_EXIT_ID = 103;
	public static final int MAX_TAG_LIST = 1024;
	
	public static final String TAG = "kr900app";
	public static final String TICKER = "New tag detected!!";
	public static final String TITLE = "KR900 LCD & Pendant";
	public static final String VERSION = " v1.4";
	
	public static String strTagItem = "N/A";
	
	private static ConnectSocket mBluetooth = null;
	private static final int REQUEST_ENABLE_BT = 2;
	
	private ListView mTagList;
	
	private Vibrator mVibe;
	private boolean mSound;
	private boolean mVibrate;
	private boolean mHardwareKey;
	private boolean mNotification;
		
	Timer timer = null;
	Timer timerReaderAlive = null;
	
	private SoundPool mSoundPool;
	private int alarm;
	
	private int mMaxCount;
	private int mTimerCount;
	
	private Button mReadStart;
	private Button mReadStop;
	private Button mClear;
	private Button mVersion;
	private Button mConfig;
	private Button mReadWrite;
	private Button mLockKill;
	
	private EditText mTagCount;
	private TextView mReaderAlive;
	private AlertDialog.Builder mAlert;
	private LinearLayout mLayoutBar;
	private NotificationManager mNotifiManager;
	private PendingIntent mIntent;
	
	private ArrayList<TagData> m_arrayTagData;
		
	class LogBroadcastReceiver extends BroadcastReceiver {
	    public void onReceive(Context context, Intent intent) {
	        Log.d("ZeeReceiver", intent.toString());
	        Bundle extras = intent.getExtras();
	        for (String k : extras.keySet()) {
	            Log.d("ZeeReceiver", "    Extra: "+ extras.get(k).toString());
	        }
	    }
	}
	
	class TagViewAdapter extends ArrayAdapter<TagData>{

		private ArrayList<TagData> curArrayTagData;
		
		public TagViewAdapter(Context context, int resource,
				ArrayList<TagData> arrayTagData) {
			super(context, resource, arrayTagData);
			this.curArrayTagData = arrayTagData;
		}
		
		@Override
		public View getView(int position, View convertView, ViewGroup parent) {
			if(convertView == null)
			{
				LayoutInflater inflater = (LayoutInflater)getSystemService(Context.LAYOUT_INFLATER_SERVICE);
				convertView = inflater.inflate(R.layout.listview_layout, null);
			}
			
			TextView tvTagUID =(TextView)convertView.findViewById(R.id.idTagUID);
			TextView tvReadCount = (TextView)convertView.findViewById(R.id.idTagReadCount);
			ImageView ivTagType = (ImageView)convertView.findViewById(R.id.idTagType);
	    
			//Date readTime = new Date(curArrayTagData.get(position).TagReadTime/86400+365*70+17+9/24+2);
			//Date readTime = new Date();
			//SimpleDateFormat dateFormat = new SimpleDateFormat("HH:mm:ss", Locale.KOREA);
			
//			double tagRSSI = (double)curArrayTagData.get(position).TagRSSI*0.8 - 24;
//			
//			BigDecimal returnData = new BigDecimal(tagRSSI).setScale(1, BigDecimal.ROUND_HALF_UP);
			 
			tvTagUID.setText(curArrayTagData.get(position).TagUID);
			tvReadCount.setText(curArrayTagData.get(position).TagCount);
			tvReadCount.setTextColor(Color.parseColor("#000099"));
			
			int nTagCount = Integer.parseInt(curArrayTagData.get(position).TagCount);
			
			if(nTagCount < 10) tvTagUID.setTextColor(Color.parseColor("#000000"));
			else if(nTagCount < 30) tvTagUID.setTextColor(Color.parseColor("#000033"));
			else if(nTagCount < 50) tvTagUID.setTextColor(Color.parseColor("#000066"));
			else if(nTagCount < 70) tvTagUID.setTextColor(Color.parseColor("#000099"));
			else if(nTagCount < 100) tvTagUID.setTextColor(Color.parseColor("#0000CC"));
			else tvTagUID.setTextColor(Color.parseColor("#0000FF"));
			
			if(curArrayTagData.get(position).TagType == 2){
				ivTagType.setImageResource(R.drawable.barcode);
			}
			else
			{
				ivTagType.setImageResource(R.drawable.tag);
			}

			return convertView;
		}
	}
	
	private void SetButtonEnable(boolean enable){
		mReadStart.setEnabled(enable);
		mReadStop.setEnabled(!enable);
		mReadWrite.setEnabled(enable);
		mLockKill.setEnabled(enable);
		mConfig.setEnabled(enable);
		mVersion.setEnabled(enable);
		//mClear.setEnabled(enable);
	}
	
	private Handler confirmHandler = new Handler() {
    	@Override
    	public void handleMessage(Message msg){
    		//완료 후 실행할 처리 입력
    		mAlert.show();
    	}
    };
	
	/** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
        
        m_arrayTagData = new ArrayList<TagData>();
        
        mAlert = new AlertDialog.Builder(this);
        mAlert.setPositiveButton( "Close", new DialogInterface.OnClickListener() {
            public void onClick( DialogInterface dialog, int which) {
                dialog.dismiss();
            }
        });
    
        mTagList = (ListView)findViewById(R.id.idtagListView);
        mTagList.setItemsCanFocus(true);
        mTagList.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
        mTagList.setDivider(new ColorDrawable(0xFF00008B));
        mTagList.setDividerHeight(1);
        mTagList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
            	final ProgressDialog progDialog = new ProgressDialog(parent.getContext());                
                progDialog.setProgressStyle(ProgressDialog.STYLE_SPINNER);
                progDialog.setMessage("Please wait....");
                progDialog.show();
                
                strTagItem = m_arrayTagData.get(position).TagUID;
                
                new Thread(new Runnable(){
                	@Override
                	public void run(){
                    	mAlert.setTitle(strTagItem);
                    	mAlert.setMessage("Item : " + GetProductName(strTagItem));

                		confirmHandler.sendEmptyMessage(0);
                		
                		progDialog.dismiss();
                	}
                }).start();
            }
        });
        
        mReadStart = (Button)findViewById(R.id.idReadStart);
        mReadStop = (Button)findViewById(R.id.idReadStop);
        mClear = (Button)findViewById(R.id.idClear);
        mVersion = (Button)findViewById(R.id.idVersion);
        mConfig = (Button)findViewById(R.id.idConfig);
        mReadWrite = (Button)findViewById(R.id.idReadWrite);
        mLockKill = (Button)findViewById(R.id.idLockKill);
        mLayoutBar = (LinearLayout)findViewById(R.id.LinearLayoutBar);
        mTagCount = (EditText)findViewById(R.id.idTagCount);
        mReaderAlive = (TextView)findViewById(R.id.idReaderAlive);
        
        mReadStart.setOnClickListener(this);
        mReadStop.setOnClickListener(this);
        mClear.setOnClickListener(this);
        mVersion.setOnClickListener(this);
        mConfig.setOnClickListener(this);
        mReadWrite.setOnClickListener(this);
        mLockKill.setOnClickListener(this);
        
        mVibe = (Vibrator)getSystemService(Context.VIBRATOR_SERVICE);
        mSoundPool = new SoundPool(1, AudioManager.STREAM_MUSIC, 0);
        alarm = mSoundPool.load(this, R.raw.success, 1);
        
        mNotifiManager = (NotificationManager)getSystemService(NOTIFICATION_SERVICE);
        mIntent = PendingIntent.getActivity(KR900L.this, 0, 
        						new Intent(KR900L.this, KR900L.class), 0);
        
        //mBar.setVisibility(8);
        mLayoutBar.setVisibility(8);       

        SetButtonEnable(true);
        
        mSound = true;
        mVibrate = true;
        mHardwareKey = false;
        mNotification = false;
        
        mMaxCount = 0;
        mTimerCount = 0;
            
//        getApplicationContext().registerReceiver(receiver,
//                new IntentFilter(BluetoothDevice.ACTION_ACL_CONNECTED));
        
        mBluetooth = new ConnectSocket();
        try {
			if(mBluetooth.ConnectReader() == false)
			{
				connectBluetoothState state = mBluetooth.GetBluetoothState();
				
				if(state == connectBluetoothState.btsNotAvalibleBluetooth)
				{
					Toast.makeText(this, "Bluetooth is not available.", Toast.LENGTH_LONG).show();
			    	finish();

			        return;
				}
				else if(state == connectBluetoothState.btsNotEnableBluetooth)
				{
					Intent enableBtIntenet = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
			    	startActivityForResult(enableBtIntenet, REQUEST_ENABLE_BT);
				}
				
				Toast.makeText(this, "Connection failure.", Toast.LENGTH_LONG).show();
			}
			else
			{
				Toast.makeText(this, "Connection success.", Toast.LENGTH_LONG).show();
			}
		} catch (SecurityException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalArgumentException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (NoSuchMethodException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        
        CreateReaderAliveTimer(500);
    	
    	NotificationOnGoingMessage(null, TITLE + VERSION, "UHF RFID Portable Reader DEMO Application ");
    }

    public void CreateTimer(int elipsedTime){
    	if(timer != null) timer.cancel();
    	
    	timer = new Timer(true);
        final android.os.Handler handler = new android.os.Handler();
        
        timer.schedule(
        		new TimerTask() {
        			@Override
        			public void run() {
        				handler.post( new Runnable() { public void run() {
        					updateTagList(false);
       					}});}
        		}
        , 0, elipsedTime); // �쒖??湲곕룞�?�컙, 媛꾧�?(ms)
    }
    
    public void CreateReaderAliveTimer(int elipsedTime){
    	if(timerReaderAlive != null) timerReaderAlive.cancel();
    	
    	timerReaderAlive = new Timer(true);
        final android.os.Handler handler = new android.os.Handler();
        
        timerReaderAlive.schedule(
        		new TimerTask() {
        			@Override
        			public void run() {
        				handler.post( new Runnable() { public void run() {
        					CheckReaderAlive();
       					}});}
        		}
        , 0, elipsedTime); // �쒖??湲곕룞�?�컙, 媛꾧�?(ms)
    }
    
    public void CreateAD(String titleDialog, String buttonName, String messageDialog, OnClickListener clickInteface){
    	AlertDialog.Builder ad = new AlertDialog.Builder(this);
    	ad.setTitle(titleDialog);
    	ad.setMessage(messageDialog);
    	ad.setPositiveButton(buttonName, clickInteface);
    	
    	ad.show();
    }
    
    private void CheckReaderAlive()
    {
    	mTimerCount++;
    	
    	if(mBluetooth.GetReaderAlive() == true)
    	{
    		mReaderAlive.setText("ALIVE");
    		
    		if((mTimerCount % 2) == 1)
    			mReaderAlive.setTextColor(Color.parseColor("#00FF00"));
    		else
    			mReaderAlive.setTextColor(Color.parseColor("#7474D2"));
    	}
    	else
    	{
    		mReaderAlive.setText("DEAD");

    		if((mTimerCount % 2) == 1)
    			mReaderAlive.setTextColor(Color.parseColor("#FF0000"));
    		else
    			mReaderAlive.setTextColor(Color.parseColor("#7474D2"));
    	}
    	
    	if(mTimerCount > 50000)
    	{
    		mTimerCount = 0;
    	}
    }
    
    private void NotificationMessage(String ticker, String title, String text)
    {
    	Notification notification = new Notification(R.drawable.icon, ticker, System.currentTimeMillis());
    	
    	notification.setLatestEventInfo(KR900L.this, title, text, mIntent);
    	
    	mNotifiManager.notify(1000, notification);
    }
    
    private void NotificationOnGoingMessage(String ticker, String title, String text)
    {
    	Notification notification = new Notification(R.drawable.icon, ticker, 0);
    	
    	notification.setLatestEventInfo(KR900L.this, title, text, mIntent);
    	notification.flags = Notification.FLAG_ONGOING_EVENT;
    	
    	mNotifiManager.notify(2000, notification);
    }
    
    private String GetProductName(String strTagUID)
    {    	
    	String strProductName = "N/A";
    	
    	String strSubStr = strTagUID.substring(strTagUID.length() - 9, strTagUID.length() - 7);
    	
    	if(strSubStr.equals("00")) strProductName = "Apple";
    	else if(strSubStr.equals("01")) strProductName = "Grape";
    	else if(strSubStr.equals("02")) strProductName = "Banana";
    	else if(strSubStr.equals("03")) strProductName = "Orange";
    	
    	mBluetooth.SendString(strProductName.length(), strProductName.getBytes());

    	return strProductName;
    }
    
    public void BeginTagRead(){
    	if(!mHardwareKey) mBluetooth.BeginRead();
    	mTagList.setChoiceMode(ListView.CHOICE_MODE_NONE);
    	mReadStart.setVisibility(8);
    	mLayoutBar.setVisibility(0);
    	//if(mNotification) NotificationMessage(TICKER, "START", "Tag read start!!");
    	NotificationOnGoingMessage(null, TITLE + VERSION, "태그 읽는중...");
    	
    	CreateTimer(250);
   	
 /*   	CreateAD("�곹�?, "�쎄린以묒�", "RFID �쒓??�쎄린以�?.", new OnClickListener() {
			@Override
			public void onClick(DialogInterface dialog, int which) {
				FinishTagRead();
			}
    	});*/
    }
       
    public void FinishTagRead() {
    	if(timer != null) timer.cancel();
		timer = null;
		
		if(!mHardwareKey) mBluetooth.FinishRead();
		
		mTagList.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
		mReadStart.setVisibility(0);
		mLayoutBar.setVisibility(8);
		//if(mNotification) NotificationMessage(TICKER, "STOP", "Tag read stop!!");
		NotificationOnGoingMessage(null, TITLE + VERSION, "태그 읽기 중지됨");
    }
    
    public void updateTagList(boolean updateForce) {
    	if(mBluetooth.IsUpdate() == false && updateForce == false) return;
    	
    	int tagListCount = mBluetooth.GetTagListSize();
    	
    	for(int index = 0; index < tagListCount; index++)
		{    		
    		TagData curTagData = new TagData(mBluetooth.GetTagByIndex(index), 
    										mBluetooth.GetCountByIndex(index), 
    										mBluetooth.GetRSSIByIndex(index), 
    										mBluetooth.GetRSSILevelByIndex(index), 
    										mBluetooth.GetReadTimeByIndex(index), 
    										mBluetooth.GetReadTypeByIndex(index) 
    										);
    		
    		InsertTagData(curTagData);
		}
    	
    	mBluetooth.ClearTagList();
    	
    	int nCurCount = m_arrayTagData.size();
    	
    	if(mMaxCount < nCurCount)
    	{
    		mMaxCount = nCurCount;
    		if(mNotification) 
			{
    			mNotifiManager.cancel(1000);
    			NotificationMessage(TICKER, "New tag detected!!", "Tag Count : " + Integer.toString(nCurCount));
			}
    	}
    	
    	mTagCount.setText(Integer.toString(nCurCount));
    	
    	mTagList.setAdapter( new TagViewAdapter(this, R.layout.listview_layout, m_arrayTagData) );
        mTagList.setSelection(m_arrayTagData.size());
    }
    
    public void InsertTagData(TagData curTagData)
    {
        int nCurrentDBSize = m_arrayTagData.size();
        
        int nMidKey = 0;
        int nLeftKey = 0;
        int nRightKey = nCurrentDBSize - 1;
        
        int nResult = 1;
        
        String strListTagUID;
        
        while(nRightKey >= nLeftKey)
        {
            nMidKey = (nLeftKey + nRightKey) / 2;
            strListTagUID = m_arrayTagData.get(nMidKey).TagUID;   
            nResult = CompareTagUID(curTagData.TagUID, strListTagUID);
            
            if(nResult < 0) nRightKey = nMidKey - 1;
            else nLeftKey = nMidKey + 1;
            
            if(nResult == 0) break;
        }
        
        int nInsertPoint = nMidKey + ((nResult > 0) ? 1 : 0);
        if(nInsertPoint > nCurrentDBSize) nInsertPoint = nCurrentDBSize;
        
        if(nResult != 0){        	
        	m_arrayTagData.add(nInsertPoint, curTagData);
        	
        	if(mVibrate == true) mVibe.vibrate(60);
        }
        else
        {           	
        	TagData tempTagData = m_arrayTagData.get(nInsertPoint);
        	int nCurTagCount = Integer.parseInt(tempTagData.getTagCount()) + Integer.parseInt(curTagData.getTagCount());

        	curTagData.setTagCount(Integer.toString(nCurTagCount)); 
            m_arrayTagData.set(nInsertPoint, curTagData);  
        }
        
        if(mSound == true) mSoundPool.play(alarm, 1, 1, 0, 0, 2);
    }
    
    public int CompareTagUID(String strTagUID, String strListTagUID)
    {        
        int nResult = strTagUID.compareTo(strListTagUID);
        
        if(nResult > 0) return 1;
        else if (nResult < 0) return -1;
        else return 0;
    }
    
    private void ShowSettingDialog()
    {
    	final CharSequence[] items = { "Use Hardware Key", "Sound", "Vibrate", "Notification" };
    	final boolean[] itemsChecked = { mHardwareKey, mSound, mVibrate, mNotification };
    	
    	AlertDialog.Builder builder = new AlertDialog.Builder(this);
    	builder.setTitle("Setting");
    	builder.setMultiChoiceItems(items, itemsChecked, new DialogInterface.OnMultiChoiceClickListener() {
			
			@Override
			public void onClick(DialogInterface dialog, int which, boolean isChecked) {
				// TODO Auto-generated method stub
				if(items[which].equals("Use Hardware Key"))
				{
					mHardwareKey = isChecked;
				}
				else if(items[which].equals("Sound"))
				{
					mSound = isChecked;
				}
				else if(items[which].equals("Vibrate"))
				{
					mVibrate = isChecked;
				}
				else if(items[which].equals("Notification"))
				{
					mNotification = isChecked;
				}
			}
		});
    	
    	builder.setPositiveButton(android.R.string.ok, null);
    	
    	AlertDialog alert = builder.create();
    	alert.show();
    }
    
	public void onClick(View v) {
		mVibe.vibrate(30);
		switch(v.getId()){
			case R.id.idReadStart 	: BeginTagRead();
									  SetButtonEnable(false);
									break;
			case R.id.idReadStop 	: FinishTagRead();
									  SetButtonEnable(true);
									break;
			case R.id.idClear 		: mBluetooth.ClearTagList();
									  m_arrayTagData.clear();
									  if(mNotification) mNotifiManager.cancel(1000);
									  mMaxCount = 0;
									  updateTagList(true);
									break;
			case R.id.idVersion		: String strReaderVersion = null;
									  strReaderVersion = mBluetooth.GetReaderVersion();
									  
									  if(!strReaderVersion.equals(""))
									  {									      
										  CreateAD("Version", "OK", "Reader Ver: " + strReaderVersion
												   + "\nJNI: " + mBluetooth.GetLibVersion(), null);
									  }
									  else
									  {
										  CreateAD("Info", "OK", "Fail to get Version", null);
									  }
									break;
			case R.id.idConfig		: Intent intentConfig = new Intent(KR900L.this, readerConfig.class);		
									  startActivity(intentConfig);
									break;
			case R.id.idReadWrite	: Intent intentReadWrite = new Intent(KR900L.this, ReadWrite.class);		
			  						  startActivity(intentReadWrite);
			  						  break;	
			case R.id.idLockKill	: Intent intentLockKill = new Intent(KR900L.this, LockKill.class);		
									  startActivity(intentLockKill);
									  break;	
		}
		
	}
	
	@Override
	public void onBackPressed (){
	    AlertDialog.Builder ad = new AlertDialog.Builder(this);
	    ad.setMessage("Do you want to exit?");
	    ad.setCancelable( false);
	    ad.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
	    	public void onClick(DialogInterface dialog, int id) {
	    		 ActivityManager am = (ActivityManager) getSystemService(ACTIVITY_SERVICE);
	        	 am.restartPackage(getPackageName());
	        	 FinishTagRead();
	        	 
	        	 connectBluetoothState state = mBluetooth.GetBluetoothState();
		        	
	        	 if(state == connectBluetoothState.btsCompleteConnectBluetooth)
	        	 {
	        		 mBluetooth.DisconnectReader();
	        	 }
	        	 
	        	 mNotifiManager.cancel(1000);
	        	 mNotifiManager.cancel(2000);
	        	 System.exit(0);
	    	}
	    });
	    ad.setNegativeButton("No",	null);
	    AlertDialog alert = ad.create();
	    // Title for AlertDialog
	    alert.setTitle("Question");
	    // Icon for AlertDialog
//	    alert.setIcon(R.drawable.icon);
	    alert.show();
	}
	
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        super.onCreateOptionsMenu(menu);

        menu.add(0, MENU_ABOUT_ID, 0, R.string.menu_about)
        	.setIcon(R.drawable.phone);
        menu.add(0, MENU_SETTING_ID, 0, R.string.menu_setting)
        	.setIcon(R.drawable.settings);
        menu.add(0, MENU_EXIT_ID, 0, R.string.menu_exit)
        	.setIcon(R.drawable.exit);
        
        return true; 
    }
    
    @Override   
    public boolean onOptionsItemSelected(MenuItem item){
         switch(item.getItemId()){
	         case MENU_ABOUT_ID:
	        	 CreateAD("About", "OK", TITLE + "\nUHF RFID Portable Reader DEMO Application" + VERSION, null);
	             return true;
	         case MENU_SETTING_ID:
	        	 ShowSettingDialog();
	        	 if(mNotification == false) mNotifiManager.cancel(1000);
	             return true;
	         case MENU_EXIT_ID:
	        	 ActivityManager am = (ActivityManager) getSystemService(ACTIVITY_SERVICE);
	        	 am.restartPackage(getPackageName());
	        	 FinishTagRead();
	        	 
	        	 connectBluetoothState state = mBluetooth.GetBluetoothState();
		        	
	        	 if(state == connectBluetoothState.btsCompleteConnectBluetooth)
	        	 {
	        		 mBluetooth.DisconnectReader();
	        	 }
	        	 
	        	 mNotifiManager.cancel(1000);
	        	 mNotifiManager.cancel(2000);
	        	 System.exit(0);
	          	 return true;
         }
         
         return false;
    }

    @Override
    public void onResume() {
        super.onResume();
        updateTagList(true);
    }

    @Override
    public void onPause() {
        super.onPause();
    }

    @Override
    public void onStop() {
        super.onStop();
    }

    @Override
    public void onDestroy(){
        super.onDestroy();
        FinishTagRead();
    }
  
    class TagData {
		private String TagUID;
		private String TagCount;
		private int TagRSSI;
		private int TagRSSILevel;
		private String TagReadTime;
		private int TagType;
		
		public TagData(String _TagUID, String _TagCount, int _TagRSSI, int _TagRSSILevel, 
				String _TagReadTime, int _TagType) {
			this.TagUID = _TagUID;
			this.TagCount = _TagCount;
			this.TagRSSI = _TagRSSI;
			this.TagRSSILevel = _TagRSSILevel;
			this.TagReadTime = _TagReadTime;
			this.TagType = _TagType;
		}
		
		public String getTagUID(){
			return TagUID;
		}
		
		public String getTagCount(){
			return TagCount;
		}
		
		public int getTagRSSI(){
			return TagRSSI;
		}
		
		public int getTagRSSILevel(){
			return TagRSSILevel;
		}
		
		public String getTagReadTime(){
			return TagReadTime;
		}
		
		public int getTagType(){
			return TagType;
		}
		
		public void setTagUID(String _TagUID){
			this.TagUID = _TagUID;
		}
		
		public void setTagCount(String _TagCount){
			this.TagCount = _TagCount;
		}
		
		public void setTagRSSI(int _TagRSSI){
			this.TagRSSI = _TagRSSI;
		}
		
		public void setTagRSSILevel(int _TagRSSILevel){
			this.TagRSSILevel = _TagRSSILevel;
		}
		
		public void setTagReadTime(String _TagReadTime){
			this.TagReadTime = _TagReadTime;
		}
		
		public void setTagType(int _TagType){
			this.TagType = _TagType;
		}
	}
}