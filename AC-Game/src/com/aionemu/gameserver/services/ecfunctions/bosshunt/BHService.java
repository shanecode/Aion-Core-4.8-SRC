package com.aionemu.gameserver.services.ecfunctions.bosshunt;

import java.util.Random;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.aionemu.commons.utils.Rnd;
import com.aionemu.gameserver.ai2.AI2Engine;
import com.aionemu.gameserver.ai2.AIState;
import com.aionemu.gameserver.ai2.AbstractAI;
import com.aionemu.gameserver.model.TeleportAnimation;
import com.aionemu.gameserver.model.gameobjects.Creature;
import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.model.gameobjects.state.CreatureSeeState;
import com.aionemu.gameserver.model.team2.group.PlayerGroup;
import com.aionemu.gameserver.model.team2.group.PlayerGroupService;
import com.aionemu.gameserver.model.templates.spawns.SpawnTemplate;
import com.aionemu.gameserver.model.utils3d.FFA3D;
import com.aionemu.gameserver.network.aion.serverpackets.SM_ITEM_USAGE_ANIMATION;
import com.aionemu.gameserver.services.ecfunctions.PVPManager;
import com.aionemu.gameserver.services.instance.InstanceService;
import com.aionemu.gameserver.services.teleport.TeleportService2;
import com.aionemu.gameserver.skillengine.model.SkillTargetSlot;
import com.aionemu.gameserver.spawnengine.SpawnEngine;
import com.aionemu.gameserver.utils.PacketSendUtility;
import com.aionemu.gameserver.utils.ThreadPoolManager;
import com.aionemu.gameserver.world.WorldMapInstance;

/**
 * @author Ghostfur
 * not finished yet Ely and Asmo Need to Spawn in sepperate Instances
 */
public class BHService extends PVPManager {
	
	private final Logger log = LoggerFactory.getLogger(BHService.class);
	private static final BHService service = new BHService();
	
	private int worldNumber;
	public String worldName;
	private FFA3D loc;

	private int randomNum = 0;
	
    private SpawnTemplate spawnEly;
    private SpawnTemplate spawnAsmo;
   
    private WorldMapInstance instance;
    private WorldMapInstance oldInstance;
    
    /**
	 * BossHunt World ID's
	 * 
	 */
    public int BossHuntID1 = 300040000; //Dark Poeta
    public int BossHuntID2 = 300220000; //Abyssal Splinter
    public int BossHuntID3 = 300030000; //Nochsana
    public int BossHuntID4 = 300190000; //Taloc's Hollow
    public int BossHuntID5 = 300610000; //Raksang Ruins

    WorldMapInstance mediumInstance1, mediumInstance2, mediumInstance3, mediumInstance4, mediumInstance5;
        
	
    public static BHService getInstance(){
        return service;
    }    
	public void Init() {		
		mediumInstance1 = createInstance(BossHuntID1, false);
		mediumInstance2 = createInstance(BossHuntID2, false);		
		mediumInstance3 = createInstance(BossHuntID3, false);
		mediumInstance4 = createInstance(BossHuntID4, false);
		mediumInstance5 = createInstance(BossHuntID5, false);
		instanceTask(80);
    	randomMediumInstance();
    	spawnPortal();
	} 
	/**
	 * Portal Locations ELY/ASMO
	 * 
	 */
	private void spawnPortal() {
		spawnEly = SpawnEngine.addNewSpawn(110010000, 730662, 1496, 1498, 565, (byte) 47, 0);
		spawnEly.setMasterName("BOSSHUNT-" + worldName);
        SpawnEngine.spawnObject(spawnEly, 1);
        AI2Engine.getInstance().setupAI("bosshunt_portal", (Creature) spawnEly.getVisibleObject());
        ((AbstractAI) ((Creature) spawnEly.getVisibleObject()).getAi2()).setStateIfNot(AIState.IDLE);
        
        spawnAsmo = SpawnEngine.addNewSpawn(120010000, 730662, 1353, 1359, 208, (byte) 95, 0);
        spawnAsmo.setMasterName("BOSSHUNT-" + worldName);
        SpawnEngine.spawnObject(spawnAsmo, 1);
        AI2Engine.getInstance().setupAI("bosshunt_portal", (Creature) spawnAsmo.getVisibleObject());
        ((AbstractAI) ((Creature) spawnAsmo.getVisibleObject()).getAi2()).setStateIfNot(AIState.IDLE);
	}	
	
	private WorldMapInstance createInstance(int worldId, boolean withDoor) {
    	if (withDoor) {
    		WorldMapInstance Instance = InstanceService.getNextAvailableSoloInstance(worldId, true);
    		return Instance;
    	} else {
    		WorldMapInstance Instance = InstanceService.getNextAvailableSoloInstance(worldId, false);
    		return Instance;
    	}
    }
	/**
	 * BossHunt Spawn Positions
	 * 
	 */
		//Dark Poeta 300040000
			static FFA3D[] posMedium1 = new FFA3D[]{
				new FFA3D(722.40234f, 440.34232f, 103.73857f, 48),};
			//Abyssal Splinter 300220000
			static FFA3D[] posMedium2 = new FFA3D[]{
				new FFA3D(347.43463f, 741.6064f, 197.2789f, 49),};
			//Nochsana Training Camp 300030000
			static FFA3D[] posMedium3 = new FFA3D[]{
				new FFA3D(342.3624f, 332.31348f, 380.01395f, 49),};
			//Taloc's Hollow 300190000
			static FFA3D[] posMedium4 = new FFA3D[]{
				new FFA3D(567.2518f, 828.97205f, 1374.8302f, 49),};
			//Raksang Ruins 300610000
			static FFA3D[] posMedium5 = new FFA3D[]{
				new FFA3D(662.52765f, 668.46985f, 522.0487f, 49),};
				private void randomLoc () {
							
		if (worldNumber == BossHuntID1) { 
			loc = posMedium1[Rnd.get(posMedium1.length - 1)];
		} else if (worldNumber == BossHuntID2) {
			loc = posMedium2[Rnd.get(posMedium2.length - 1)];
		} else if (worldNumber == BossHuntID3) { 
			loc = posMedium3[Rnd.get(posMedium3.length - 1)];
		} else if (worldNumber == BossHuntID4) { 
			loc = posMedium4[Rnd.get(posMedium4.length - 1)];
		} else if (worldNumber == BossHuntID5) { 
			loc = posMedium5[Rnd.get(posMedium5.length - 1)];
		}
	}
	private int getRandomNum() {
		return randomNum;
	}
	private void setRandomNum(int randomNum) {
		this.randomNum = randomNum;
	}
	private void randomMediumInstance() {	
	   	Random r = new Random();
	   	int  i = r.nextInt(5) + 1;
	   	if (getRandomNum() == 0) {
	   		setRandomNum(i);
	   	} else {
		   	while (i == randomNum) {
				i = r.nextInt(5) + 1;
			}
		   	setRandomNum(i);
	   	}
		/**
		 * BossHunt WorldNames/WorldNumbers/Instance
		 * 
		 */
		if (i == 1) {
			worldName = "BERITRA"; //Dark Poeta 
			worldNumber = 300040000;
			instance = mediumInstance1;
	    } else if (i == 2) {
			worldName = "HYPERION"; //Abyssal Splinter
			worldNumber = 300220000;
			instance = mediumInstance2; 
	    } else if (i == 3) {
			worldName = "DYNATOUM"; //Nochsana Training Camp
			worldNumber = 300030000;
			instance = mediumInstance3; 
	    } else if (i == 4) {
			worldName = "TAHABATA"; //Taloc's Hollow
			worldNumber = 300190000;
			instance = mediumInstance4; 
	    } else if (i == 5) {
			worldName = "PRECTAZ"; //Raksang Ruins
			worldNumber = 300610000;
			instance = mediumInstance5; 
		    } 
	    } 	
    private void changeMap(final WorldMapInstance inst) {        	

    	randomMediumInstance();
    	
    	log.info("Rotating Bosshunt Map: " + worldNumber + " - " + worldName +"");
        for (Player p : inst.getPlayersInside()) {
        	PacketSendUtility.sendPacket(p, new SM_ITEM_USAGE_ANIMATION(p.getObjectId(), 0, 0, (int) TimeUnit.SECONDS.toMillis(10), 0, 0));
        	PacketSendUtility.sendWhiteMessageOnCenter(p, "Bosshunt: Rotating map. All players will be teleport in 10 seconds.");
    	 	paralizePlayer(p, true);
        }
    
        ThreadPoolManager.getInstance().schedule(new Runnable() {
            @Override
            public void run() {
        		for (final Player p : inst.getPlayersInside()) {
        			paralizePlayer(p, false);
        			if (p.isOnline() && !p.getLifeStats().isAlreadyDead()) {        					
    					if (p.isInGroup2()) {
    						PlayerGroup group = p.getPlayerGroup2();
    						if (group.isLeader(p)) {
    							randomLoc();
    							for (Player member : group.getMembers()) {   
    								PacketSendUtility.broadcastPacketAndReceive(member, new SM_ITEM_USAGE_ANIMATION(p.getObjectId(), 0, 0, 0, 1, 0));
    								TeleChangeMap(member);        								
    							}
    						}        						
    					} else {			
    						PacketSendUtility.broadcastPacketAndReceive(p, new SM_ITEM_USAGE_ANIMATION(p.getObjectId(), 0, 0, 0, 1, 0));
        			    	randomLoc();
        					TeleChangeMap(p);   
    					}
    				}
    			}    
        		spawnPortal();
            }
        }, (int) TimeUnit.SECONDS.toMillis(10));  
    }        
   	private void TeleChangeMap (Player player) {
    	TeleportService2.teleportTo(player, worldNumber, instance.getInstanceId(), (float) loc.x, (float) loc.y, (float) loc.z, (byte) loc.h, TeleportAnimation.JUMP_AIMATION_3);
	    HealPlayer(player, false, true);
   	}   	
    private ScheduledFuture<?> instanceTask(int delayInMinutes) {
    	return ThreadPoolManager.getInstance().scheduleAtFixedRate(new Runnable() {	
    		@Override
    		public void run() {    
				oldInstance = instance;		
				deSpawnPortal(spawnAsmo);
				deSpawnPortal(spawnEly);
				changeMap(oldInstance);     	
    		}
    	}, delayInMinutes / 2 * 1000 * 60, delayInMinutes / 2 * 1000 * 60);  
    }      	
    public void TeleIn(Player player) {
    		randomLoc(); 	  
    		//Enter BossHunt With Group  
         	if (player.isInGroup2()) {    	  	
        	PlayerGroup group = player.getPlayerGroup2();
    		for (Player member : group.getMembers()) {          	
		    	TeleportService2.teleportTo(member, worldNumber, instance.getInstanceId(), (float) loc.x, (float) loc.y, (float) loc.z, (byte) loc.h, TeleportAnimation.JUMP_AIMATION_3);;   
		    	resetStart(member);
    		}      		
         }  
    } 
     public void TeleOut(Player player) {	
     	resetEnd(player);
     		//Leave BossHunt 
         if (player.isInGroup2()) {
             PlayerGroupService.removePlayer(player);
         }
         if (player.world() != 0) {
             TeleportService2.teleportTo(player, player.world(), player.locX(), player.locY(), player.locZ(), (byte) player.locH(), TeleportAnimation.JUMP_AIMATION_3);
         }
         else {
             TeleportService2.moveToBindLocation(player, true);
         }
         player.clearPrevLoc();    
     }        

     private void resetEnd(Player player) {
         player.setInBH(false);
         AddProtection(player, 10 * 1000);
         player.getController().cancelCurrentSkill();
         player.getEffectController().removeAbnormalEffectsByTargetSlot(SkillTargetSlot.DEBUFF);
         if (player.getAccessLevel() > 4) {
 			player.setInvul(true);
 			player.setAdminNeutral(3);
 			player.setAdminEnmity(0);
 			player.setSeeState(CreatureSeeState.SEARCH10);
 		}	
 		HealPlayer(player, false, true);
        //PacketSendUtility.sendSpecMessage("BOSSHUNT", player.getName() + " has left the battle.");
     }
     
     private void resetStart(Player player) {
     	player.setInBH(true);
     	InstanceService.registerPlayerWithInstance(instance, player);
     	AddProtection(player, 10 * 1000);
     	if (player.getAccessLevel() > 4) {
 			player.setInvul(false);
 			player.setAdminNeutral(0);
 			player.unsetSeeState(CreatureSeeState.SEARCH10);
 		}	
     	HealPlayer(player, false, true);
     }
 }
