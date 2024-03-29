using System;
using System.Net;
using System.Threading;
using System.Text;
using System.IO;
using UnityEngine;

// ClientGetAsync issues the async request.
public class ClientGetAsync 
{
	//public static ManualResetEvent allDone = new ManualResetEvent(false);
	const int BUFFER_SIZE = 1024;

	public delegate void callback(string s);
	public event callback Callback;

	public ClientGetAsync(callback c)
	{
		this.Callback = c;
	}


	public void getRequest(string urlS)
	{
		if (urlS==null || urlS.Length==0){
			return ;
		}
		
		// Get the URI from the command line.
		Uri httpSite = new Uri(urlS);
		
		// Create the request object.
		WebRequest wreq = WebRequest.Create(httpSite);
		
		// Create the state object.
		RequestState rs = new RequestState();
		
		// Put the request into the state object so it can be passed around.
		rs.Request = wreq;
		
		// Issue the async request.
		IAsyncResult r = (IAsyncResult) wreq.BeginGetResponse(new AsyncCallback(RespCallback), rs);
	}
	
	private void RespCallback(IAsyncResult ar)
	{
		// Get the RequestState object from the async result.
		RequestState rs = (RequestState) ar.AsyncState;
		
		// Get the WebRequest from RequestState.
		WebRequest req = rs.Request;
		
		// Call EndGetResponse, which produces the WebResponse object
		//  that came from the request issued above.
		WebResponse resp = req.EndGetResponse(ar);         
		
		//  Start reading data from the response stream.
		Stream ResponseStream = resp.GetResponseStream();
		
		// Store the response stream in RequestState to read 
		// the stream asynchronously.
		rs.ResponseStream = ResponseStream;
		
		//  Pass rs.BufferRead to BeginRead. Read data into rs.BufferRead
		IAsyncResult iarRead = ResponseStream.BeginRead(rs.BufferRead, 0,BUFFER_SIZE, new AsyncCallback(ReadCallBack), rs); 
	}
	
	
	private void ReadCallBack(IAsyncResult asyncResult)
	{
//		// Get the RequestState object from AsyncResult.
//		RequestState rs = (RequestState)asyncResult.AsyncState;
//		
//		// Retrieve the ResponseStream that was set in RespCallback. 
//		Stream responseStream = rs.ResponseStream;
//		
//		// Read rs.BufferRead to verify that it contains data. 
//		int read = responseStream.EndRead( asyncResult );
//		if (read > 0)
//		{
//			// Prepare a Char array buffer for converting to Unicode.
//			Char[] charBuffer = new Char[BUFFER_SIZE];
//			
//			// Convert byte stream to Char array and then to String.
//			// len contains the number of characters converted to Unicode.
//			int len = 
//				rs.StreamDecode.GetChars(rs.BufferRead, 0, read, charBuffer, 0);
//			
//			String str = new String(charBuffer, 0, len);
//			
//			// Append the recently read data to the RequestData stringbuilder
//			// object contained in RequestState.
//			rs.RequestData.Append(
//				Encoding.ASCII.GetString(rs.BufferRead, 0, read));         
//			
//			// Continue reading data until 
//			// responseStream.EndRead returns –1.
//			IAsyncResult ar = responseStream.BeginRead( 
//			                                           rs.BufferRead, 0, BUFFER_SIZE, 
//			                                           new AsyncCallback(ReadCallBack), rs);
//		}
//		else
//		{
//			if(rs.RequestData.Length>0)
//			{
//				//  Display data to the console.
		//				string strContent;     aitOne             
//				strContent = rs.RequestData.ToString();
//			}
//			// Close down the response stream.
//			responseStream.Close();         
//			// Set the ManualResetEvent so the main thread can exit.
//			allDone.Set();                           
//		}

		Debug.Log("test done");


		if(Callback != null)
			Callback("hello");

		return;
	}    
}

// The RequestState class passes data across async calls.
public class RequestState
{
	const int BufferSize = 1024;
	public StringBuilder RequestData;
	public byte[] BufferRead;
	public WebRequest Request;
	public Stream ResponseStream;
	// Create Decoder for appropriate enconding type.
	public Decoder StreamDecode = Encoding.UTF8.GetDecoder();
	
	public RequestState()
	{
		BufferRead = new byte[BufferSize];
		RequestData = new StringBuilder(String.Empty);
		Request = null;
		ResponseStream = null;
	}     
}