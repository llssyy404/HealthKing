package com.kr900l;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

import java.util.Set;
import android.util.Log;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;

public class ConnectSocket {
	private static Boolean mConnectBluetooth = false;
	private static Thread mThreadProc = null;

	private static InputStream mmInStream;
	private static OutputStream mmOutStream;
	
	private void GetSocketStream(BluetoothSocket socket) {
		InputStream tmpIn = null;
		OutputStream tmpOut = null;

		// Get the input and output streams, using temp objects because
		// member streams are final
		try {
			tmpIn = socket.getInputStream();
			tmpOut = socket.getOutputStream();
		} catch (IOException e) {
		}

		mmInStream = tmpIn;
		mmOutStream = tmpOut;
	}
	
	public enum connectBluetoothState{
		btsNone,
		btsNotAvalibleBluetooth,
		btsNotEnableBluetooth,
		btsNotPairedBluetooth,
		btsFailRfConnect,
		btsFailBluetoothSocket,
		btsCompleteConnectBluetooth,
	}	
	
    private static BluetoothAdapter mBluetoothAdapter = null;
    private BluetoothSocket mSocket = null;
    private BluetoothSocket tmp = null;
    
//   private static final UUID MY_UUID =
//    	UUID.fromString("00001101-0000-1000-8000-00805F9B34FB");
    
    private static connectBluetoothState mBluetoothState = connectBluetoothState.btsNone;
    
    private static int mTagCurrentUpdateCount = 0;
    private static int mTagOldUpdateCount = 0; 

    public Boolean ConnectReader() throws SecurityException, NoSuchMethodException, IllegalArgumentException, IllegalAccessException, InvocationTargetException{
    	
        mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
        
        if (mBluetoothAdapter == null) 
        {
        	mBluetoothState = connectBluetoothState.btsNotAvalibleBluetooth;
        	Log.i("ConnectSocket", mBluetoothState.toString());
        	return false;
        }
        
        if (!mBluetoothAdapter.isEnabled()) 
        {
        	mBluetoothState = connectBluetoothState.btsNotEnableBluetooth;
        	Log.i("ConnectSocket", mBluetoothState.toString());
        	return false;
        }
        
        Set<BluetoothDevice> pairedDevices = mBluetoothAdapter.getBondedDevices();
        
        // If there are paired devices
	     if (pairedDevices.size() > 0) {
	         // Loop through paired devices
	         for (BluetoothDevice device : pairedDevices) {
// 				 Add the name and address to an array adapter to show in a ListView
	        	 
	        	 Log.v("ConnectSocket", "Bluetooth Device Name : " + device.getName() + " State : " + device.getBondState() );
				 
	        	 if(device.getBondState() != BluetoothDevice.BOND_BONDED) continue;
	        	 if(device.getName().contains("RFID") != true && device.getName().contains("ESD100") != true && device.getName().contains("KR-900") != true) continue;
	        	 
				 if( mSocket != null){
					 
					 try {
						mSocket.close();
						mSocket = null;
					} catch (IOException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					 
				 }
				 
				 	if( tmp != null){
					 
					 try {
						 tmp.close();
						 tmp = null;
					} catch (IOException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					 
				 }
				 /*
				 // Get a BluetoothSocket to connect with the given BluetoothDevice
				 try {
				     // MY_UUID is the app's UUID string, also used by the server code
				     tmp = device.createRfcommSocketToServiceRecord(MY_UUID);
				 } catch (IOException e) {
					 mBluetoothState = connectBluetoothState.btsNotPairedBluetooth;
					 return false;
				 }
				 mSocket = tmp;*/
				 
				 Method m = device.getClass().getMethod("createInsecureRfcommSocket", new Class[] {int.class});          
				 mSocket = (BluetoothSocket) m.invoke(device, 1); 
				 
				 mBluetoothAdapter.cancelDiscovery();

				 try {
				    // Connect the device through the socket. This will block
				 // until it succeeds or throws an exception
					 mSocket.connect();

				 } catch (IOException connectException) {
				    // Unable to connect; close the socket and get out
					 try {
						 mSocket.close();
						 mSocket = null;
					 } catch (IOException closeException) { }
				 
					 continue;
				 }
				 
				 mBluetoothState = connectBluetoothState.btsCompleteConnectBluetooth;
				 
				 GetSocketStream(mSocket);
				 
				 Initialize();
				 
				 mConnectBluetooth = true;
				 class ProcessSocketData implements Runnable {
				    public void run() {
				    	while(mConnectBluetooth == true){
				    		mTagCurrentUpdateCount = RFIDProcessData();
				    	}
				    }
				 }
				 
				 mThreadProc = new Thread(new ProcessSocketData(), "ConnectSocket_Thread");
				 mThreadProc.start();
				 				 
				 return true;
	         }
	         
			 mBluetoothState = connectBluetoothState.btsFailRfConnect;
			 return false;
	     }
	     
	     mBluetoothState = connectBluetoothState.btsNotPairedBluetooth;
	     return false;
    }
    
    /** Will cancel an in-progress connection, and close the socket */
    public void DisconnectReader() {
        try {
        	mConnectBluetooth = false;
        	mThreadProc = null;
        	
        	mmInStream.close();
			mmOutStream.close();
        	
            mSocket.close();
            mBluetoothAdapter = null;
            mSocket = null;
            
        } catch (IOException e) { }
    }
    
    public connectBluetoothState GetBluetoothState(){ 
    	return mBluetoothState; 
    }
    
	/* Call this from the main Activity to send data to the remote device */
	public int read(byte[] buffer) {
		int readsize = 0;

		try {
			readsize = mmInStream.read(buffer);
		} catch (IOException e) {
		}

		return readsize;
	}

	/* Call this from the main Activity to send data to the remote device */
	public void write(byte[] bytes) {
		try {
			mmOutStream.write(bytes);
		} catch (IOException e) {
		}
	}

	/* Native Method */
	private native int RFIDInitializeReader();
	private native int RFIDProcessData();

	// Tag Read Start/Stop
	private native int RFIDStartReading();
	private native int RFIDEndReading();
	
	private native String[] RFIDGetTagUIDList();
	private native String[] RFIDGetTagUIDCountList();
	private native int RFIDGetTagUIDListSize();
	
	// Tag Data
	private native String RFIDGetTagUIDByIndex(int nIndex);
	private native String RFIDGetTagUIDCountByIndex(int nIndex);
	private native int RFIDGetTagRSSIByIndex(int nIndex);
	private native int RFIDGetTagRSSILevelByIndex(int nIndex);
	private native String RFIDGetTagReadTimeByIndex(int nIndex);
	private native int RFIDGetTagTypeByIndex(int nIndex);
	
	private native void RFIDClearTagUIDList();
	
	// Tag Memory
	private native byte[] RFIDReadMemory(int nBytePos, int nByteSize);
	private native int RFIDWriteMemory(int nBytePos, int nByteSize, byte[] writeData);
	private native int RFIDLock(int nPayload, byte[] AccessPassword);
	private native int RFIDKill(byte[] AccessPassword, byte[] KillPassword);
	
	// Q Value
    private native int RFIDSetGen2QValue(int nQValue);
    private native int RFIDGetGen2QValue();

    // RF Attenuation
    private native int RFIDSetRfPowerAttenuation(int nAttenuation);
    private native int RFIDGetRfPowerAttenuation();
    
    // Register Read/Write/Save/Default
    private native int RFIDReadRegister(int nAddr);
    private native int RFIDWriteRegister(int nAddr, int nValue);
    private native int RFIDSaveRegister();
    private native int RFIDDefaultRegister();
    
    // Send String
    private native int RFIDSendString(int nSize, byte[] SendData);
    
    // Reader Alive
    private native int RFIDGetReaderAlive();
    private native void RFIDSetReaderAliveTimeOut(int nTimeOut);
	
    // Version
	private native String RFIDGetLiberyVesion();
	private native String RFIDGetReaderVersion();
	
	// Password
	private native void RFIDSetAccessPassword(int nPassword);
	private native int RFIDGetAccessPassword();
	private native void RFIDSetKillPassword(int nPassword);
	private native int RFIDGetKillPassword();
	
	public boolean Initialize(){
		int returnvalue = RFIDInitializeReader();
		
		return ((returnvalue == 1) ? true : false); 
	}
	
	public boolean WriteRegister(int nAddress, int nValue){
		int nRetVal = RFIDWriteRegister(nAddress, nValue);
		
		return ((nRetVal == 1) ? true : false); 
	}
	
	public int ReadRegister(int nAddress){
		return RFIDReadRegister(nAddress);
	}
	
	public boolean BeginRead(){
		int returnvalue = RFIDStartReading();
		
		return ((returnvalue == 1) ? true : false);
	}
	
	public void FinishRead() {
		RFIDEndReading();
	}

	public String[] GetTagList(){
		return RFIDGetTagUIDList();
	}
	
	public String[] GetCountList(){
		return RFIDGetTagUIDCountList();
	}
	
	public int GetTagListSize(){
		return RFIDGetTagUIDListSize();
	}
	
	public String GetTagByIndex(int nIndex){
		return RFIDGetTagUIDByIndex(nIndex);
	}
	public String GetCountByIndex(int nIndex){
		return RFIDGetTagUIDCountByIndex(nIndex);
	}
	
	public int GetRSSIByIndex(int nIndex){
		return RFIDGetTagRSSIByIndex(nIndex);
	}
	
	public enum RSSILevel{
		rssiHighest,
		rssiHigh,
		rssiLow,
		rssiLowest,
	}
	
	public int GetRSSILevelByIndex(int nIndex){
		return RFIDGetTagRSSILevelByIndex(nIndex);
	}
	
	public String GetReadTimeByIndex(int nIndex){
		return RFIDGetTagReadTimeByIndex(nIndex);
	}
	
	public enum TagType{
		tagC1G2,
		tag6B,
		tagBarcode,
	}
	
	public int GetReadTypeByIndex(int nIndex){
		return RFIDGetTagTypeByIndex(nIndex);
	}
	
	public void ClearTagList(){
		RFIDClearTagUIDList();
	}

	public boolean IsUpdate(){
		if(mTagCurrentUpdateCount > mTagOldUpdateCount)
		{
			mTagOldUpdateCount = mTagCurrentUpdateCount;
			return true;
		}
		
		mTagOldUpdateCount = mTagCurrentUpdateCount;
		
		return false;
	}
	
	public boolean SendString(int nSize, byte[] SendData)
	{
		return ((RFIDSendString(nSize, SendData) == 1) ? true : false);
	}
	
	public String GetReaderVersion(){
		return RFIDGetReaderVersion();
	}
	
	public String GetLibVersion(){
		return RFIDGetLiberyVesion();
	}
		
	public boolean ReadTagMemory(int nBytePos, int nByteSize, byte[] readData){
		RFIDSetAccessPassword(0);
		byte[] ReadResult = RFIDReadMemory(nBytePos, nByteSize);
		
		System.arraycopy(ReadResult, 1, readData, 0, nByteSize);
		
		return ((ReadResult[0] == 1) ? true : false);
	}
	
	public byte ReadTagMemoryWithPassword(int nBytePos, int nByteSize, byte[] readData, int accessPassword){
		RFIDSetAccessPassword(accessPassword);
		byte[] ReadResult = RFIDReadMemory(nBytePos, nByteSize);
		
		System.arraycopy(ReadResult, 1, readData, 0, nByteSize);
		
		return ReadResult[0];
	}
	
	public boolean WriteTagMemory(int nBytePos, int nByteSize, byte[] writeData){
		RFIDSetAccessPassword(0);
		int returnvalue = RFIDWriteMemory(nBytePos, nByteSize, writeData);
		
		return ((returnvalue == 1) ? true : false); 
	}
	
	public boolean WriteTagMemoryWithPassword(int nBytePos, int nByteSize, byte[] writeData, int accessPassword){
		RFIDSetAccessPassword(accessPassword);
		int returnvalue = RFIDWriteMemory(nBytePos, nByteSize, writeData);
		
		return ((returnvalue == 1) ? true : false); 
	}
		
	public boolean LockTag(int nPayload, byte[] AccessPassword){
		int returnvalue = RFIDLock(nPayload, AccessPassword);
		
		return ((returnvalue == 1) ? true : false); 
	}
	
	public boolean KillTag(byte[] AccessPassword, byte[] KillPassword){
		int returnvalue = RFIDKill(AccessPassword, KillPassword);
		
		return ((returnvalue == 1) ? true : false); 	
	}
	
	public boolean SetMemoryBank(int nBank){
		if(nBank >= 0 && nBank <= 4)
		{
			int nRetVal = RFIDWriteRegister(0x2A, nBank);
			return ((nRetVal == 1) ? true : false); 
		}

		return false;
	}
	
	public boolean SetGen2QValue(int nQValue){
		return ((RFIDSetGen2QValue(nQValue) == 1) ? true : false); 
	}
	
	public boolean SetRfPowerAttenuation(int nAttenuation){
		if(nAttenuation >= -3 && nAttenuation <= 30)
		{
			return ((RFIDSetRfPowerAttenuation(nAttenuation) == 1) ? true : false);
		}

		return false;
	}
	
	public int GetMemoryBank(){
		return RFIDReadRegister(0x2A);
	}
	
	public int GetGen2QValue(){	/* 0 - 16 */
		return RFIDGetGen2QValue();
	}
	
	public int GetRfPowerAttenuation(){	/* 0 - 30 */
		return RFIDGetRfPowerAttenuation();
	}
	
	public boolean GetReaderAlive(){
		return ((RFIDGetReaderAlive() == 1) ? true : false);
	}
	
	public void SetReaderAliveTimeOut(int nTimeOut){
		RFIDSetReaderAliveTimeOut(nTimeOut);
	}
	
	public boolean SaveRegister(){
		return ((RFIDSaveRegister() == 1) ? true : false);
	}
	
	public boolean DefaultRegister(){
		return ((RFIDDefaultRegister() == 1) ? true : false);
	}
	
	public void SetAccessPassword(int nPassword){
		RFIDSetAccessPassword(nPassword);
	}

	public int GetAccessPassword(){
		return RFIDGetAccessPassword();
	}

	public void SetKillPassword(int nPassword){
		RFIDSetKillPassword(nPassword);
	}

	public int GetKillPassword(){
		return RFIDGetKillPassword();
	}
	
	static {
		try {
			System.loadLibrary("ubists");
		} catch (Exception e) {
			System.out.println(e.toString());
		}
	}
}
