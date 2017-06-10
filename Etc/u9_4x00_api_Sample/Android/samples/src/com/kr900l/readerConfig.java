package com.kr900l;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class readerConfig extends Activity implements View.OnClickListener {
	private static ConnectSocket mBluetooth = null;
	
	private Button mSetQValue;
	private Button mSetAtten;
	private EditText mQValue;
	private EditText mRFAtten;
	private Button mHS;
	private Button mHR;
	private EditText mAddr;
	private EditText mValue;
	private Button mSend;
	private EditText mAscii;
	//private Button mSave;
	//private Button mDefault;
	
	/** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.config);
        
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
        
        mSetQValue = (Button)findViewById(R.id.idSetQ);
        mSetAtten = (Button)findViewById(R.id.idSetAtten);
        mHS = (Button)findViewById(R.id.idButtonHS);
        mHR = (Button)findViewById(R.id.idButtonHR);
        mSend = (Button)findViewById(R.id.idButtonSend);
        //mSave = (Button)findViewById(R.id.idSave);
        //mDefault = (Button)findViewById(R.id.idDefault);
        
        mSetQValue.setOnClickListener(this);
        mSetAtten.setOnClickListener(this);
        mHS.setOnClickListener(this);
        mHR.setOnClickListener(this);
        mSend.setOnClickListener(this);
        //mSave.setOnClickListener(this);
        //mDefault.setOnClickListener(this);
        
        mQValue = (EditText)findViewById(R.id.editQValue);
        mRFAtten = (EditText)findViewById(R.id.editRFAtten);
        mAddr = (EditText)findViewById(R.id.idEditAddress);
        mValue = (EditText)findViewById(R.id.idEditValue);
        mAscii = (EditText)findViewById(R.id.idEditAscii);
        
        mBluetooth = new ConnectSocket();
        
        mQValue.setText( Integer.toString(mBluetooth.GetGen2QValue()) );
        mRFAtten.setText( Integer.toString(mBluetooth.GetRfPowerAttenuation()) );
    }
    
    private void SetupRegister(int nAddr, int nValue)
    {
    	if(mBluetooth.WriteRegister(nAddr, nValue) == true){
    		Toast.makeText(this, "Register write success.", Toast.LENGTH_LONG).show();
    	}
    	else{
    		Toast.makeText(this, "Register write failure.", Toast.LENGTH_LONG).show();
    	}
    }
    
    private void ReadRegister(int nAddr)
    {
    	int nValue = mBluetooth.ReadRegister(nAddr);
    	
    	if(mBluetooth.ReadRegister(nAddr) != 0xFFFF)
    	{
    		Toast.makeText(this, "Register read success.", Toast.LENGTH_LONG).show();
    		mValue.setText(Integer.toHexString(nValue));
    	}
    	else
    	{
    		Toast.makeText(this, "Register read failure.", Toast.LENGTH_LONG).show();
    	}
    }
    
    private void SendString(String strAscii)
    {
    	if(mBluetooth.SendString(strAscii.length(), strAscii.getBytes()) == true)
    	{
    		Toast.makeText(this, "Ascii data send success.", Toast.LENGTH_LONG).show();
    	}
    	else
    	{
    		Toast.makeText(this, "Ascii data send failure.", Toast.LENGTH_LONG).show();
    	}
    }
    
    public void onClick(View v) {
		switch(v.getId()){
		case R.id.idSetAtten : mBluetooth.SetRfPowerAttenuation( Integer.parseInt(mRFAtten.getText().toString()) );
							break;
		case R.id.idSetQ : mBluetooth.SetGen2QValue( Integer.parseInt(mQValue.getText().toString()) );
							break;
		case R.id.idButtonHS : SetupRegister(Integer.valueOf(mAddr.getText().toString(), 16), Integer.valueOf(mValue.getText().toString(), 16));
							break;
		case R.id.idButtonHR : ReadRegister(Integer.valueOf(mAddr.getText().toString(), 16));
							break;
		case R.id.idButtonSend : SendString(mAscii.getText().toString());
							break;
		//case R.id.idSave : mBluetooth.SaveRegister();
		//					break;
		//case R.id.idDefault : mBluetooth.DefaultRegister();
		//					break;
		}
    }
}
