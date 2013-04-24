/**
 * City generator 
 * Randomly generates level and populates with buildings
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityGen : MonoBehaviour {
	private static float scalingFactor = 0.4F;
	private static float mapSize = 100F;
	public float prob = 1F;
	
	public static GameObject ground = (GameObject) Resources.Load("ground");
	public static GameObject building = (GameObject) Resources.Load("building");
	protected Stack<Block> open = new Stack<Block>();

	void Start () {
		GameObject gameGround = (GameObject) Instantiate(ground, new Vector3(0F, 0F, 0F), Quaternion.identity);
		gameGround.transform.localScale = new Vector3(mapSize, 0F, mapSize);
		ground.transform.localScale = new Vector3(1, 1, 1);
		open.Push(new Block(new Vector3(mapSize/scalingFactor, 0F, mapSize/scalingFactor), new Vector3(-mapSize/scalingFactor, 0F, -mapSize/scalingFactor)));
		splitLevel();
	}
	
	/** method splits the ground plane based on a decreasing probability
	 * from this roads will be created
	 */
	void splitLevel() {
		while (open.Count > 0) //while there are still more items in the stack
		{
			if (Random.Range(0f, 1f) < prob) {
				foreach (Block b in open.Pop().split()) {
					open.Push(b);
				}
				
				prob *= .7F;
			} else {
				open.Pop().generate();
			}
		}
	}
	
	/**
	 * Contains definitions for each block on the grid
	 */
	public class Block {
		private Vector3 topLeft;
		private Vector3 bottomRight;
		public float probability;
		
		//constructor 
		public Block(Vector3 topLeft, Vector3 bottomRight) {
			this.topLeft = topLeft;
			this.bottomRight = bottomRight;
			this.probability = 1F;
		}
		
		
		//
		public List<Block> split() {
			List<Block> toReturn = new List<Block>();
			if (Random.Range (0f,1f) > .5) {
				print("Splitting Vertically");
				//split vertical
				float newX = Mathf.Abs((bottomRight.x - topLeft.x) / 2);
				Vector3 upper = new Vector3(newX, topLeft.y, topLeft.z);
				Vector3 lower = new Vector3(newX, bottomRight.y, bottomRight.z);
				//create the new blocks
				Block left = new Block(this.topLeft, lower);
				Block right = new Block(upper, this.bottomRight);
				//add to the set
				toReturn.Add(left);
				toReturn.Add(right);
			} else {
				print("Splitting Horizontally");
				//split horizontal
				float newZ = Mathf.Abs((bottomRight.z - topLeft.z) / 2);
				Vector3 left = new Vector3(topLeft.x, topLeft.y, newZ);
				Vector3 right = new Vector3(bottomRight.x, bottomRight.y, newZ);
				//create new blocks
				Block top = new Block(this.topLeft, right);
				Block bottom = new Block(left, this.bottomRight);
				//add to the set
				toReturn.Add(top);
				toReturn.Add(bottom);
			}
			
			probability *= Random.Range (.8f,.9f); //hardcoded for now
			
			return toReturn;
		}
		
		/**
		 * Method to generate the level based on the stack of blocks
		 */
		public void generate() {
			//find where buildings need to be
			Vector3 top = new Vector3(this.topLeft.x - 1, this.topLeft.y, this.topLeft.z - 1);
			Vector3 bottom = new Vector3(this.bottomRight.x + 1, this.bottomRight.y, this.bottomRight.z + 1);
			for (int i = 1; i < 7; i++) {
				//place building prefab
				GameObject newBuilding = (GameObject) Instantiate(building, new Vector3(Random.Range (top.x, bottom.x), 0F, Random.Range (top.z, bottom.z)), Quaternion.identity);
				//scale randomly
				newBuilding.transform.localScale = new Vector3(Random.Range (top.x, bottom.x)/3F, Random.Range (150,250), Random.Range (top.z, bottom.z)/3F);
			}
		}
	}
	
}
