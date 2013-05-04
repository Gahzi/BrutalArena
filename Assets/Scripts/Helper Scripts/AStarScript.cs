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
		new Vector2(0,1), 
		new Vector2(-1,1),
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
		return GetPathBetweenTwoTiles(origTile,destTile).Count - 1;
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
		}
		Debug.Log("Couldn't find shortest path from orig: " + origTile.tileCoordinate.ToString() + " to dest: " + destTile.tileCoordinate.ToString());
		return null;
		
	}
	
	private void CalculateNeighbourWeights(AStarTile centerATile) {
		TileScript neighbourTile = null;
		List<int> rowCounts = map.GetRowCounts();

		foreach(Vector2 relNeighbourCoord in tileNeighbours) {
			Vector2 neighbourCoord = centerATile.GetTileCoordinate() + relNeighbourCoord;

			if(neighbourCoord.y >= 0 && neighbourCoord.y <= 12) { 
				int currentRowCount = rowCounts[(int)centerATile.GetTileCoordinate().y];
				int destRowCount = rowCounts[(int)neighbourCoord.y];
				if(currentRowCount < destRowCount && (relNeighbourCoord.y == 1 || relNeighbourCoord.y == -1)) {
					neighbourCoord.x += 1;
				}
			}
			
			neighbourTile = (TileScript)map.GetTiles()[neighbourCoord];
			
			if(neighbourTile != null) {
				AStarTile neighbourATile = new AStarTile(neighbourCoord);
				neighbourATile.SetParent(centerATile);
				neighbourATile.SetWeight(centerATile.GetWeight() + 1);
				
				if(closedList.Contains(neighbourATile)) continue;
				if(neighbourTile.GetTileInhabitant() != null) {
					neighbourATile.SetWeight(99);
				}
				
				if(openList.Contains(neighbourATile)) {
					AStarTile previousNeighbourATile = openList[openList.IndexOf(neighbourATile)];
					int newCost = previousNeighbourATile.GetWeight() - previousNeighbourATile.GetParent().GetWeight() + centerATile.GetWeight() + 1;
					if(previousNeighbourATile.GetWeight() > newCost) {
						previousNeighbourATile.SetParent(centerATile);
						previousNeighbourATile.SetWeight(newCost);
					}
					
				}
				else {
					openList.Add (neighbourATile);
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

