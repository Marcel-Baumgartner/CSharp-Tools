using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NextUtilities.Config
{

    public class ConfigReader 
    {
    	string filename;
    	string seperator;
    	bool output;
    	List<string> keys = new List<string>();
    	List<string> values = new List<string>();
    	
        public ConfigReader(string fileName)
        {
        	filename = fileName;
        	seperator = "=";
        	output = false;
        }
    	public ConfigReader(string fileName, string Seperator)
    	{
    		filename = fileName;
    		seperator = Seperator;
    		output = false;
    	}
    	public ConfigReader(string fileName, string Seperator, bool Output)
    	{
    		filename = fileName;
    		seperator = Seperator;
    		output = Output;
    	}
    	public void ReadConfig()
    	{
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (sr.Peek() != -1)
                {
                    string[] data = sr.ReadLine().Split(Convert.ToChar(seperator));
                    keys.Add(data[0]);
                    values.Add(data[1]);
                }
                sr.Close();
            }
            catch(IOException ex)
            {
                Console.WriteLine("Error with config: " + ex);
            }
    	}
    	public string GetKeysValue(string key)
    	{
    		int i = 0;
    		string result = "null";
    		if(output)
    			Console.WriteLine("[NextUtilities] (ConfigReader) Reading config !");
    		foreach(string var in keys)
    		{
    			if(var == key)
    			{
    				result = values[i];
    			}
    			i++;
    		}
    		if(result == "null")
    		{
    			throw new InvalidDataException(String.Format("Key {0} not found in file '{1}'", key, filename));
    		}
    		return result;
    	}
    	public bool KeyExist(string key)
    	{
    		bool result = false;
    		foreach(string var in keys)
    		{
    			if(var == key)
    			{
    				result = true;
    				break;
    			}
    		}
    		return result;
    	}
    	public string[] GetAllKeys()
    	{
    		//Ist wie eine Liste
    		string[] result = keys.ToArray();
    		return result;
    	}
    	public List<string> GetAllKeysAsList()
    	{
    		List<string> result = new List<string>();
    		foreach(string var in keys)
    		{
    			result.Add(var);
    		}
    		return result;
    	}
    }
}