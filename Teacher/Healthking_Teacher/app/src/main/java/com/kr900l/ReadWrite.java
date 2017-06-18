//package com.kr900l;
//
//import android.app.Activity;
//import android.os.Bundle;
//import android.view.View;
//import android.view.WindowManager;
//import android.widget.ArrayAdapter;
//import android.widget.Button;
//import android.widget.CheckBox;
//import android.widget.CompoundButton;
//import android.widget.CompoundButton.OnCheckedChangeListener;
//import android.widget.EditText;
//import android.widget.Spinner;
//
//public class ReadWrite extends Activity implements View.OnClickListener, OnCheckedChangeListener{
//	private static ConnectSocket mBluetooth = null;
//	Spinner spinBank;
//
//	EditText mStartPos;
//	EditText mSize;
//	EditText mReadMemoryValue;
//	EditText mWriteMemoryValue;
//	EditText mRWStatus;
//
//	Button mReadMemory;
//	Button mWriteMemory;
//
//	EditText mAccessPassword;
//	CheckBox mUsePassword;
//
//	byte[] memoryData = new byte[256];
//	boolean mSetAccessPassword;
//
//	/** Called when the activity is first created. */
//    @Override
//    public void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        setContentView(R.layout.read_write);
//
//        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
//
//        mBluetooth = new ConnectSocket();
//
//        spinBank = (Spinner)findViewById(R.id.idBank);
//        spinBank.setPrompt("Select Bank");
//
//        ArrayAdapter<CharSequence> arraySpinnerItems = ArrayAdapter.createFromResource(this, R.array.bank,
//        		android.R.layout.simple_spinner_item);
//
//        spinBank.setAdapter(arraySpinnerItems);
//        spinBank.setSelection(mBluetooth.GetMemoryBank());
//
//        mStartPos = (EditText)findViewById(R.id.idStartPos);
//        mSize = (EditText)findViewById(R.id.idSize);
//        mReadMemoryValue = (EditText)findViewById(R.id.idReadMemoryValue);
//        mWriteMemoryValue = (EditText)findViewById(R.id.idWriteMemoryValue);
//        mRWStatus = (EditText)findViewById(R.id.idRWStatus);
//
//        mReadMemory = (Button)findViewById(R.id.idReadMemory);
//        mWriteMemory = (Button)findViewById(R.id.idWriteMemory);
//
//        mReadMemory.setOnClickListener(this);
//        mWriteMemory.setOnClickListener(this);
//
//        mAccessPassword = (EditText)findViewById(R.id.idAccessPassword);
//
//        mUsePassword = (CheckBox)findViewById(R.id.idcheckUsePassword);
//        mUsePassword.setOnCheckedChangeListener((OnCheckedChangeListener) this) ;
//
//        SetPasswordToControl();
//
//        mSetAccessPassword = false;
//    }
//
//    @Override
//    public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
//    	mSetAccessPassword = isChecked;
//    }
//
//    private void ReadMemory(){
//    	mBluetooth.SetMemoryBank(spinBank.getSelectedItemPosition());
//
//    	int BytePos = Integer.parseInt(mStartPos.getText().toString());
//		int ByteSize = Integer.parseInt(mSize.getText().toString());
//
//		boolean readOK = false;
//		byte readErrorCode = 0;
//
//		if(mSetAccessPassword){
//			readErrorCode = mBluetooth.ReadTagMemoryWithPassword(BytePos, ByteSize, memoryData, GetPasswordFromControl());
//		}
//		else{
//			readOK = mBluetooth.ReadTagMemory(BytePos, ByteSize, memoryData);
//		}
//
//		mReadMemoryValue.setText("");
//		mRWStatus.setText("Reading...");
//
//		if(readOK == true || readErrorCode == 1){
//			mRWStatus.setText("Read OK!");
//
//			String ReadValue = "";
//
//			for(int Pos = 0; Pos < ByteSize; Pos++){
//				ReadValue += String.format("%02X ", memoryData[Pos]);
//			}
//			mReadMemoryValue.setText(ReadValue);
//		}
//		else
//		{
//			if(mSetAccessPassword)
//			{
//				mRWStatus.setText("Read Fail!(" + readErrorCode + ")");
//			}
//			else
//			{
//				mRWStatus.setText("Read Fail!");
//			}
//		}
//    }
//
//    private void WriteMemory(){
//    	mBluetooth.SetMemoryBank(spinBank.getSelectedItemPosition());
//
//    	int BytePos = Integer.parseInt(mStartPos.getText().toString());
//		int ByteSize = Integer.parseInt(mSize.getText().toString());
//
//		String WriteValue = "#" + mWriteMemoryValue.getText().toString().replace(" ", "#");
//
//		for(int Pos = 0; Pos < ByteSize; Pos++){
//			int decodeValue = 0;
//			try{
//				decodeValue = Integer.decode(WriteValue.substring(Pos*3, Pos*3+3));
//			}
//			catch(NumberFormatException e){
//				decodeValue = 0;
//			}
//
//			memoryData[Pos] = (byte)decodeValue;
//		}
//
//		mRWStatus.setText("Writing...");
//
//		boolean writeOK = false;
//
//		if(mSetAccessPassword){
//			writeOK = mBluetooth.WriteTagMemoryWithPassword(BytePos, ByteSize, memoryData, GetPasswordFromControl());
//		}
//		else{
//			writeOK = mBluetooth.WriteTagMemory(BytePos, ByteSize, memoryData);
//		}
//
//		if(writeOK == true){
//			mRWStatus.setText("Write OK!");
//		}
//		else mRWStatus.setText("Write Fail!");
//    }
//
//    private void SetPasswordToControl(){
//    	String ReadValue = "";
//    	int accessPassword = mBluetooth.GetAccessPassword();
//
//		for(int Pos = 3; Pos >= 0; Pos--){
//			ReadValue += String.format("%02X ", (accessPassword >> (Pos*8) & 0xFF));
//		}
//		mAccessPassword.setText(ReadValue);
//    }
//
//    private int GetPasswordFromControl(){
//    	int passWord = 0;
//
//		String WriteValue = "#" + mAccessPassword.getText().toString().replace(" ", "#");
//
//		for(int Pos = 0; Pos < 4; Pos++){
//			int decodeValue = 0;
//			try{
//				decodeValue = Integer.decode(WriteValue.substring(Pos*3, Pos*3+3));
//			}
//			catch(NumberFormatException e){
//				decodeValue = 0;
//			}
//
//			passWord |= decodeValue << ((3-Pos)*8);
//		}
//
//		return passWord;
//    }
//
//    public void onClick(View v) {
//		switch(v.getId()){
//		case R.id.idReadMemory :
//							ReadMemory();
//							break;
//		case R.id.idWriteMemory :
//							WriteMemory();
//							break;
//		}
//    }
//
//}
