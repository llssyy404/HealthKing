package com.example.administrator.healthking_teacher;

        import com.kr900l.ConnectSocket;
        import java.io.IOException;
        import java.io.InputStream;
        import java.io.OutputStream;
        import java.lang.reflect.InvocationTargetException;
        import java.lang.reflect.Method;

        import java.util.Set;

        import android.os.Debug;
        import android.util.Log;

        import android.bluetooth.BluetoothAdapter;
        import android.bluetooth.BluetoothDevice;
        import android.bluetooth.BluetoothSocket;

/**
 * Created by Administrator on 2017-06-18.
 */

public class RFIDDevice {

    static private RFIDDevice _instance;
    static public RFIDDevice getInstance() {
        if (_instance == null) {
            _instance = new RFIDDevice();
            _instance.Init();
        }
        return _instance;
    }

    private ConnectSocket connectSocket;

    private void Init()
    {
        connectSocket = new ConnectSocket();
    }

    public boolean set() {

        try {
            if (connectSocket.ConnectReader() == false) {
                ConnectSocket.connectBluetoothState state = connectSocket.GetBluetoothState();

                if (state == ConnectSocket.connectBluetoothState.btsNotAvalibleBluetooth) {
                    //Toast.makeText(this, "Bluetooth is not available.", Toast.LENGTH_LONG).show();
                    //finish();
                    return true;
                } else if (state == ConnectSocket.connectBluetoothState.btsNotEnableBluetooth) {
                    //Intent enableBtIntenet = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
                    // startActivityForResult(enableBtIntenet, REQUEST_ENABLE_BT);
                }

                //Toast.makeText(this, "Connection failure.", Toast.LENGTH_LONG).show();
            } else {
                //Toast.makeText(this, "Connection success.", Toast.LENGTH_LONG).show();
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
        }
        catch (InvocationTargetException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return  false;
    }

    public void beginReadTag()
    {
        connectSocket.BeginRead();
    }

    public void finishReadTag()
    {
        connectSocket.FinishRead();
        connectSocket.ClearTagList();

    }

    public String[] getTagList()
    {
        return connectSocket.GetTagList();
    }

    public int getRfPowerAttenuation(){return connectSocket.GetRfPowerAttenuation();}
    public boolean SetRfPowerAttenuation(int nAttenuation){return connectSocket.SetRfPowerAttenuation(nAttenuation);}
    public int GetTagListSize(){return connectSocket.GetTagListSize();}
    public String[] GetCountList(){return connectSocket.GetCountList();}
    public void DisconnectReader() {
        connectSocket.DisconnectReader();
    }
}
