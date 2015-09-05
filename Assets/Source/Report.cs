﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

namespace TrafficReport
{
	
	[Serializable]
	public enum EntityType {
		Citizen,
		Vehicle
	}

	[Serializable]
	public struct PathPoint {
        public Vector3 pos, forwards, backwards;
        public uint segmentId, laneId;
        public bool guessed;
	}
	
	[Serializable]
	public struct EntityInfo
	{
		public EntityType type;
		public PathPoint[] path;
		public uint id;

		public uint sourceBuilding;
		public uint targetBuilding;

		public string serviceType;

	}

    [Serializable]
    public class Report
    {
        public EntityInfo[] allEntities;

		public Report() {
		}

		public Report(EntityInfo _info)
		{
			allEntities = new EntityInfo[]{_info};
		}

        public Report(EntityInfo[] _info)
        {
            allEntities = _info;
        }
		       

        public void Save(string name)
        {
			Log.info ("Saving report");
			try 
			    {
			    XmlSerializer xml = new XmlSerializer (typeof(Report));
                FileStream fs = new FileStream(name, FileMode.Create, FileAccess.Write);
			    xml.Serialize (fs,this);
                fs.Close();
			} catch(Exception e) {
				Log.error("Error saving report" + e.ToString());
            }
            finally {
            }
            
        }

        public static Report Load(string name)
        {
			XmlSerializer xml = new XmlSerializer (typeof(Report));
			return xml.Deserialize(new FileStream(name, FileMode.Open, FileAccess.Read)) as Report;
        }

    }
}
