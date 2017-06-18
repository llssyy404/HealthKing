//package com.kr900l;
//
//import android.app.Activity;
//import android.os.Bundle;
//import android.view.View;
//import android.view.WindowManager;
//import android.widget.Button;
//import android.widget.EditText;
//import android.widget.Spinner;
//
//public class LockKill extends Activity implements View.OnClickListener {
//	private static ConnectSocket mBluetooth = null;
//	Spinner spinBank;
//
//	EditText mAccessPassword;
//	EditText mKillPassword;
//	EditText mKillAccessPassword;
//	EditText mLockPayloadMask;
//	EditText mLockPayloadAct;
//	EditText mKillLockStatus;
//
//	Button mKillTag;
//	Button mLockTag;
//
//	/** Called when the activity is first created. */
//    @Override
//    public void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        setContentView(R.layout.lock_kill);
//
//        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
//
//        mBluetooth = new ConnectSocket();
//
//        mAccessPassword = (EditText)findViewById(R.id.idAccessPass);
//        mKillPassword = (EditText)findViewById(R.id.idKillPass);
//        mKillAccessPassword = (EditText)findViewById(R.id.idKillAccessPass);
//        mLockPayloadMask = (EditText)findViewById(R.id.idPaloadMask);
//        mLockPayloadAct = (EditText)findViewById(R.id.idPayloadAct);
//        mKillLockStatus = (EditText)findViewById(R.id.idKillLockStatus);
//
//        mKillTag = (Button)findViewById(R.id.idKill);
//        mLockTag = (Button)findViewById(R.id.idLock);
//
//        mKillTag.setOnClickListener(this);
//        mLockTag.setOnClickListener(this);
//    }
//
//    private void KillTag(){
//    	byte[] killPassword = new byte[4];
//    	byte[] killAccessPassword = new byte[4];
//
//    	String KillPass = "#" + mKillPassword.getText().toString().replace(" ", "#");
//    	String KillAccessPass = "#" + mKillAccessPassword.getText().toString().replace(" ", "#");
//
//		for(int Pos = 0; Pos < 4; Pos++){
//			int decodeKillPass = 0;
//			int decodeKillAccessPass = 0;
//			try{
//				decodeKillPass = Integer.decode(KillPass.substring(Pos*3, Pos*3+3));
//				decodeKillAccessPass = Integer.decode(KillAccessPass.substring(Pos*3, Pos*3+3));
//			}
//			catch(NumberFormatException e){
//
//			}
//
//			killPassword[Pos] = (byte)decodeKillPass;
//			killAccessPassword[Pos] = (byte)decodeKillAccessPass;
//		}
//
//		mKillLockStatus.setText("Killing...");
//
//		boolean killOK = mBluetooth.KillTag(killAccessPassword, killPassword);
//
//		if(killOK == true){
//			mKillLockStatus.setText("Kill OK!");
//		}
//		else mKillLockStatus.setText("Kill Fail!");
//    }
//
//    private void LockTag() {
//    	byte[] AccessPassword = new byte[4];
//
//    	String AccessPass = "#" + mAccessPassword.getText().toString().replace(" ", "#");
//
//		for(int Pos = 0; Pos < 4; Pos++){
//			int decodeAccessPass = 0;
//			try{
//				decodeAccessPass = Integer.decode(AccessPass.substring(Pos*3, Pos*3+3));
//			}
//			catch(NumberFormatException e){
//
//			}
//
//			AccessPassword[Pos] = (byte)decodeAccessPass;
//		}
//
//		int nPayload = 0;
//
//		String payloadMask = mLockPayloadMask.getText().toString().replace(" ", "");
//		String payloadAct = mLockPayloadAct.getText().toString().replace(" ", "");
//
//		for(int maskBit = 0; maskBit < 10; maskBit++){
//			nPayload |= Integer.decode(payloadMask.substring(maskBit, maskBit+1)) << (19-maskBit);
//		}
//		for(int actBit = 0; actBit < 10; actBit++){
//			nPayload |= Integer.decode(payloadAct.substring(actBit, actBit+1)) << (9-actBit);
//		}
//
//		mKillLockStatus.setText("Locking...");
//
//		boolean lockOK = mBluetooth.LockTag(nPayload, AccessPassword);
//
//		if(lockOK == true){
//			mKillLockStatus.setText("Lock OK!");
//		}
//		else mKillLockStatus.setText("Lock Fail!");
//    }
//
//    public void onClick(View v) {
//		switch(v.getId()){
//		case R.id.idKill :
//							KillTag();
//							break;
//		case R.id.idLock :
//							LockTag();
//							break;
//
//		}
//    }
//}
