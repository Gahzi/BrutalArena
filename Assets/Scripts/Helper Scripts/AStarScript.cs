using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarScript {
	TileMapScript map;
	List<AStarTile> openList;
	List<AStarTile> closedList;
	
	Vector2[] tileNeighbours = { 
		new Vector2(0,-1), 
		new Vector2(1,0),
		new Vector2(1,1), 
		new Vector2(0,1),
		new Vector2(-1,0), 
		new Vector2(-1,-1)
	};
	
	
	public AStarScript(TileMapScript tileMap) {
		map = tileMap;
		Hashtable mapTiles = map.GetTiles();		
		
		openList = new List<AStarTile>(mapTiles.Count);
		closedList = new List<AStarTile>(mapTiles.Count);
		
	}
	public List<Vector2> GetPathBetweenTwoTiles(TileScript origTile, TileScript destTile) {
		List<Vector2> pathList = new List<Vector2>();
		AStarTile finalTile = FindShortestPathBetweenTwoTiles(origTile,destTile);
		
		while(finalTile != null) {
			pathList.Add(finalTile.GetTileCoordinate());
			finalTile = finalTile.GetParent();
		}
		
		pathList.Reverse();
		return pathList;
	}
	
	public int GetRangeBetweenTwoTiles(TileScript origTile, TileScript destTile) {
		return FindShortestPathBetweenTwoTiles(origTile,destTile).GetWeight();
	}
	
	private AStarTile FindShortestPathBetweenTwoTiles(TileScript origTile, TileScript destTile) {
		openList.Clear();
		closedList.Clear();
		
		if(map.GetTiles().Contains(origTile.tileCoordinate)) {
			AStarTile startingAStarTile = new AStarTile(origTile.tileCoordinate);
			startingAStarTile.SetWeight(0);
			openList.Add(startingAStarTile);
			
			while(openList.Count > 0) {
				
				//grab lowest costing node
				AStarTile lowestTile = GetLowestCostTile();
				
				if(lowestTile.GetTileCoordinate() == destTile.tileCoordinate) {
					return lowestTile;
				}
				
				//move it to closed list
				openList.Remove(lowestTile);
				closedList.Add(lowestTile);
				//TileScript addingTile = (TileScript)map.GetTiles()[lowestTile.GetTileCoordinate()];
				//addingTile.gameObject.GetComponent<tk2dSprite>().color = Color.blue;
				
				//calculate weight of adjacents
				CalculateNeighbourWeights(lowestTile);
			}
			//at this point you have full map with weight aka ranges
			//range check on a certain tile is possible if given starting position
			//TODO: Save A* results unless move
			//grab all 6 nearby neighbours and calculate their weight.
			//add starting tile to finishedList
			//add 6 neighbours in the recalculate list
			//iterate through each element in the recalculate list and calculat their neighbours weight.
		}
		Debug.Log("Couldn't find shortest path from orig: " + origTile.tileCoordinate.ToString() + " to dest: " + destTile.tileCoordinate.ToString());
		return null;
		
	}
	
	private void CalculateNeighbourWeights(AStarTile centerTile) {
		foreach(Vector2 relNeighbourCoord in tileNeighbours) {
			Vector2 neighbourCoord = centerTile.GetTileCoordinate() + relNeighbourCoord;
			if(map.GetTiles().Contains(neighbourCoord)) {
				AStarTile neighbourTile = new AStarTile(neighbourCoord);
				neighbourTile.SetParent(centerTile);
				neighbourTile.SetWeight(centerTile.GetWeight() + 1);
				
				if(closedList.Contains(neighbourTile)) continue;
				
				if(openList.Contains(neighbourTile)) {
					AStarTile previousNeighbourTile = openList[openList.IndexOf(neighbourTile)];
					int newCost = previousNeighbourTile.GetWeight() - previousNeighbourTile.GetParent().GetWeight() + centerTile.GetWeight() + 1;
					if(previousNeighbourTile.GetWeight() > newCost) {
						previousNeighbourTile.SetParent(centerTile);
						previousNeighbourTile.SetWeight(newCost);
					}
					
				}
				else {
					openList.Add (neighbourTile);
					//TileScript addingTile = (TileScript)map.GetTiles()[neighbourTile.GetTileCoordinate()];
					//addingTile.gameObject.GetComponent<tk2dSprite>().color = Color.red;
				}	
			}
		}
		
	}
	
	private AStarTile GetLowestCostTile() {
		openList.Sort(new OpenListComparer());
		int lowestIndex = openList.Count-1;
		return openList[lowestIndex];
	}
	
	public class AStarTile {
		Vector2 tileCoordinate;
		int weight;
		AStarTile parent;
		//TODO: Change from weight to g value + heuristic
		
		public AStarTile(Vector2 newTileCoordinate) {
			tileCoordinate = newTileCoordinate;
			weight = 99;
			parent = null;
			//TODO: Investigate Infinity value?	
		}

		public override int GetHashCode() 
   		{
      		return tileCoordinate.x.GetHashCode() ^ tileCoordinate.y.GetHashCode();
   		}
   		
		public static bool operator ==(AStarTile x, AStarTile y) 
   		{
				if(object.ReferenceEquals(x, null) &&
        		   object.ReferenceEquals(y, null))
    				{
						return true;
					}
				else if (object.ReferenceEquals(x, null) ||
        			object.ReferenceEquals(y, null))
    				{
        				return false;
   				 	}
				else {
					return x.tileCoordinate == y.tileCoordinate;	
				}
				
		}
   		
		public static bool operator !=(AStarTile x, AStarTile y) 
   		{
      		return !(x == y);
   		}
		
		public override bool Equals(System.Object o) 
  		{
      		return o.GetType() == typeof(AStarTile) && this == (AStarTile)o;
   		}
		
		public Vector2 GetTileCoordinate() {
			return tileCoordinate;	
		}
			
		public void SetWeight(int newWeight) {
			weight = newWeight;		
		}
		
		public int GetWeight() {
			return weight;	
		}
		
		public void SetParent(AStarTile newParent) {
			parent = newParent;	
		}
		
		public AStarTile GetParent() {
			return parent;	
		}
	}
			
	private class OpenListComparer : IComparer<AStarTile> {
		
		public int Compare(AStarTile a, AStarTile b)
         {	
			if(a.GetWeight() < b.GetWeight()) {
				return 1;	
			}
			if(a.GetWeight() > b.GetWeight()) {
				return -1;	
			}
			else { return 0; }
         }
	}
}

