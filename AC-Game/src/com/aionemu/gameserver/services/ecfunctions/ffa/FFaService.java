
package com.aionemu.gameserver.services.ecfunctions.ffa;

import java.util.Random;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.aionemu.commons.utils.Rnd;
import com.aionemu.gameserver.ai2.AI2Engine;
import com.aionemu.gameserver.ai2.AIState;
import com.aionemu.gameserver.ai2.AbstractAI;
import com.aionemu.gameserver.model.ChatType;
import com.aionemu.gameserver.model.EmotionType;
import com.aionemu.gameserver.model.TeleportAnimation;
import com.aionemu.gameserver.model.gameobjects.Creature;
import com.aionemu.gameserver.model.gameobjects.Item;
import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.model.gameobjects.state.CreatureSeeState;
import com.aionemu.gameserver.model.ingameshop.InGameShopEn;
import com.aionemu.gameserver.model.templates.spawns.SpawnTemplate;
import com.aionemu.gameserver.model.utils3d.FFA3D;
import com.aionemu.gameserver.network.aion.serverpackets.SM_DIE;
import com.aionemu.gameserver.network.aion.serverpackets.SM_EMOTION;
import com.aionemu.gameserver.network.aion.serverpackets.SM_ITEM_USAGE_ANIMATION;
import com.aionemu.gameserver.network.aion.serverpackets.SM_SYSTEM_MESSAGE;
import com.aionemu.gameserver.services.abyss.AbyssPointsService;
import com.aionemu.gameserver.services.ecfunctions.PVPManager;
import com.aionemu.gameserver.services.instance.InstanceService;
import com.aionemu.gameserver.services.item.ItemService;
import com.aionemu.gameserver.services.player.PlayerReviveService;
import com.aionemu.gameserver.services.teleport.TeleportService2;
import com.aionemu.gameserver.skillengine.model.SkillTargetSlot;
import com.aionemu.gameserver.spawnengine.SpawnEngine;
import com.aionemu.gameserver.utils.PacketSendUtility;
import com.aionemu.gameserver.utils.ThreadPoolManager;
import com.aionemu.gameserver.utils.audit.AuditLogger;
import com.aionemu.gameserver.world.WorldMapInstance;
import com.aionemu.gameserver.model.Race;
import com.aionemu.gameserver.model.templates.item.ArmorType;

/**
 * @author Ghostfur 
 */
public class FFaService extends PVPManager {
	
	private final Logger log = LoggerFactory.getLogger(FFaService.class);
	private static final FFaService service = new FFaService();
	
	private int worldNumber;
	private Player Killer;
	public String worldName;
	private FFA3D loc;

	public double x, y, z;
	public int h;
	
	private int randomNum = 0;
	
    private SpawnTemplate spawnEly;
    private SpawnTemplate spawnAsmo;
   
    private WorldMapInstance instance;
    private WorldMapInstance oldInstance;
    
    /**
	 * Instance ID 
	 */
    
    public int mediumMapID1 = 300200000; // HARAMEL
    public int mediumMapID2 = 300100000; // STEELRAKE
    public int mediumMapID3 = 300700000; // HEXWAY
    public int mediumMapID4 = 320130000; // ADMASTRONGHOLD
    public int mediumMapID5 = 300110000; // CHANTRA DREDGION
    
    WorldMapInstance mediumInstance1, mediumInstance2, mediumInstance3, mediumInstance4, mediumInstance5;
	
    public static FFaService getInstance(){
        return service;
    }    
	public void Init() {		
		mediumInstance1 = createInstance(mediumMapID1, false);	
		mediumInstance2 = createInstance(mediumMapID2, false);	
		mediumInstance3 = createInstance(mediumMapID3, false);	
		mediumInstance4 = createInstance(mediumMapID4, false);
		mediumInstance5 = createInstance(mediumMapID5, false);
		instanceTask(80);
    	randomMediumInstance();
    	spawnPortal();
	} 
	
    /**
	 * Spawn Portal In City For ELY/ASMO
	 */
	
	private void spawnPortal() {
		spawnEly = SpawnEngine.addNewSpawn(110010000, 804574, 1497, 1523, 565, (byte) 47, 0);
		spawnEly.setMasterName("FFA-" + worldName);
        SpawnEngine.spawnObject(spawnEly, 1);
        AI2Engine.getInstance().setupAI("ffa_portal", (Creature) spawnEly.getVisibleObject());
        ((AbstractAI) ((Creature) spawnEly.getVisibleObject()).getAi2()).setStateIfNot(AIState.IDLE);
        
        spawnAsmo = SpawnEngine.addNewSpawn(120010000, 804574, 1264, 1443, 209, (byte) 95, 0);
        spawnAsmo.setMasterName("FFA-" + worldName);
        SpawnEngine.spawnObject(spawnAsmo, 1);
        AI2Engine.getInstance().setupAI("ffa_portal", (Creature) spawnAsmo.getVisibleObject());
        ((AbstractAI) ((Creature) spawnAsmo.getVisibleObject()).getAi2()).setStateIfNot(AIState.IDLE);
	}	
	
    /**
	 * Create Instance
	 */
	
	private WorldMapInstance createInstance(int worldId, boolean withDoor) {
    	if (withDoor) {
    		WorldMapInstance FFAInstance = InstanceService.getNextAvailableFFAInstance(worldId, true);
    		return FFAInstance;
    	} else {
    		WorldMapInstance FFAInstance = InstanceService.getNextAvailableFFAInstance(worldId, false);
    		return FFAInstance;
    	}
    }
	
	/**
	 * Medium posMedium / Spawn Locations
	 */
	
	  //Haramel 300200000
		static FFA3D[] posMedium1 = new FFA3D[]{
			new FFA3D(173.65646f, 20.642525f, 144.2249f, 48),
			new FFA3D(177.50137f, 54.633404f, 144.31184, 70),
			new FFA3D(65.592285f, 103.332886f, 139.21887f, 70),
			new FFA3D(147.95638f, 145.66255f, 144.28432f, 71),
			new FFA3D(222.8991f, 143.64795f, 137.24051f, 4),
			new FFA3D(196.22012, 224.88269f, 127.297775f, 70),
			new FFA3D(259.68097f, 214.05086, 88.951996, 10),};
		   //Steelrake  300100000
		   static FFA3D[] posMedium2 = new FFA3D[]{
		   new FFA3D(295.22342f, 594.04926f, 952.03534f, 45),
		   new FFA3D(331.15668f, 546.1082f, 952.017f, 45),
		   new FFA3D(385.88788f, 598.3644f, 946.0964f, 45), 
		   new FFA3D(314.60825f, 604.8099f, 953.8755f, 45),
		   new FFA3D(416.053f, 618.0517f, 948.10394f, 50),
		   new FFA3D(412.55984f, 561.04663f, 946.0964f, 97),
		   new FFA3D(324.9939f, 563.54517f, 953.76324f, 45),
		   new FFA3D(406.02325f, 582.67944f, 948.04694f, 45),
		   new FFA3D(404.43176f, 503.80304f, 944.6922f, 115),};
		//Hexway 300700000
		static FFA3D[] posMedium3 = new FFA3D[]{
			new FFA3D(672.7278f, 606.0872f, 321.21414f, 35),
			new FFA3D(671.12964f, 548.0154f, 335.33472f, 53),
			new FFA3D(573.454f, 574.33246f, 351.15314f, 20),
			new FFA3D(496.35623f, 607.3109f, 354.911f, 117),
			new FFA3D(281.05402f, 741.2451f, 364.93622f, 89),
			new FFA3D(329.2735f, 607.40625f, 362.06534f, 45),
			new FFA3D(502.37585f, 480.88242f, 352.70825f, 50),};
		  //Adma StrongHold 320130000
		static FFA3D[] posMedium4 = new FFA3D[]{
			new FFA3D(477.3009f, 829.53735f, 164.83f, 35),
			new FFA3D(412.77263f, 766.42993f, 157.3499f, 64),	
			new FFA3D(418.8922f, 734.38824f, 156.48161f, 53),
			new FFA3D(511.591f, 723.5784f, 158.40771f, 20),
			new FFA3D(499.9079f, 665.70013f, 162.78363f, 117),
			new FFA3D(445.4567f, 572.82654f, 162.26096f, 89),
			new FFA3D(430.8684f, 635.88104f, 163.3105f, 45),
			new FFA3D(405.17413f, 654.0031f, 168.9041f, 50),
			new FFA3D(355.19757f, 604.04974f, 168.9041f, 50),};
		 //Chantra Dredgion 300110000
		static FFA3D[] posMedium5 = new FFA3D[]{
			new FFA3D(485.1806f, 306.4124f, 402.696f, 35),
			new FFA3D(468.0433f, 583.24805f, 391.57822f, 64),
			new FFA3D(422.15012f, 467.68222f, 393.14484f, 53),
			new FFA3D(318.10907f, 403.9567f, 412.34094f, 20),
			new FFA3D(415.9837f, 567.6851f, 410.72897f, 117),
			new FFA3D(401.1345f, 693.84625f, 402.20755f, 89),
			new FFA3D(484.84583f, 771.5248f, 388.9195f, 45),
			new FFA3D(559.26385f, 698.91876f, 402.20535f, 45),
			new FFA3D(587.76495f, 540.60944f, 409.4695f, 45),
			new FFA3D(587.76495f, 540.60944f, 409.4695f, 45),
			new FFA3D(650.5343f, 410.8248f, 412.2131f, 45),
			new FFA3D(485.2726f, 859.8147f, 417.9915f, 50),};
			private void randomLoc () {

			if (worldNumber == mediumMapID1) { 
				loc = posMedium1[Rnd.get(posMedium1.length - 1)];
			} else if (worldNumber == mediumMapID2) {
				loc = posMedium2[Rnd.get(posMedium2.length - 1)];
			} else if (worldNumber == mediumMapID3) {
				loc = posMedium3[Rnd.get(posMedium3.length - 1)];
			} else if (worldNumber == mediumMapID4) {
				loc = posMedium4[Rnd.get(posMedium4.length - 1)];
			} else if (worldNumber == mediumMapID5) {
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
		if (i == 1) {
			worldName = "Haramel"; 
			worldNumber = 300200000;
			instance = mediumInstance1;
		 } else if (i == 2) {
			worldName = "Steelrake"; 
			worldNumber = 300100000;
			instance = mediumInstance2; 
		 } else if (i == 3) {
		    worldName = "Hexway";
		    worldNumber = 300700000;
		    instance = mediumInstance3; 
		 } else if (i == 4) {
		    worldName = "AdmaStrongHold";
		    worldNumber = 320130000;
		    instance = mediumInstance4;
		 } else if (i == 5) {
		    worldName = "Chantra Dredgion";
		    worldNumber = 300110000;
		    instance = mediumInstance5;
	    } 
	} 	
	
    private void changeMap(final WorldMapInstance inst) {        	

    	randomMediumInstance();
    
		log.info("Rotating FFA Map: " + worldNumber + " - " + worldName +"");
        for (Player p : inst.getPlayersInside()) {
        	PacketSendUtility.sendPacket(p, new SM_ITEM_USAGE_ANIMATION(p.getObjectId(), 0, 0, (int) TimeUnit.SECONDS.toMillis(10), 0, 0));
        	PacketSendUtility.sendWhiteMessageOnCenter(p, "FFA: Rotating map. All players will be teleport in 10 seconds.");
    	 	paralizePlayer(p, true);
        }
        
        ThreadPoolManager.getInstance().schedule(new Runnable() { 
            @Override
            public void run() {
        		for (final Player p : inst.getPlayersInside()) {
        			paralizePlayer(p, false);
        			if (p.isOnline() && !p.getLifeStats().isAlreadyDead()) {        					
    						randomLoc();
    						PacketSendUtility.broadcastPacketAndReceive(p, new SM_ITEM_USAGE_ANIMATION(p.getObjectId(), 0, 0, 0, 1, 0));
    						TeleChangeMap(p);        								    						
    					} else {
    						randomLoc();
    						PacketSendUtility.broadcastPacketAndReceive(p, new SM_ITEM_USAGE_ANIMATION(p.getObjectId(), 0, 0, 0, 1, 0));
        					TeleChangeMap(p);   
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
    public int getFFASize () {
    	return getPlayerCountFFA(instance);
    }   	
    
    /**
	 * Teleport Players In
	 */
    
    public void TeleIn(Player player) {
    	randomLoc(); 
    	if (player.isInFFA()) {
    		TeleportService2.teleportTo(player, worldNumber, instance.getInstanceId(), (float) loc.x, (float) loc.y, (float) loc.z, (byte) loc.h, TeleportAnimation.JUMP_AIMATION_3);
    		HealPlayer(player, false, true);
            AddProtection(player, 10 * 1000);
            player.getEffectController().removeAbnormalEffectsByTargetSlot(SkillTargetSlot.DEBUFF);
    	} else {
        	resetStart(player);
        	TeleportService2.teleportTo(player, worldNumber, instance.getInstanceId(), (float) loc.x, (float) loc.y, (float) loc.z, (byte) loc.h, TeleportAnimation.JUMP_AIMATION_3);	        
    	}      		
        log.info(String.format("[FFA] %s Enter - Map: %s", player.getName(), worldName));
        if (instance != null && getPlayerCountFFA(instance) > 1) {
        	PacketSendUtility.sendSpecMessage("FFA", player.getName() + " has joined Free For All with " + getPlayerCountFFA(instance) +" players.");
        } else {
        	PacketSendUtility.sendSpecMessage("FFA", player.getName() + " has joined Free For All.");
        }
    }    
    
    /**
	 * Teleport Players Out
	 */
    
    public void TeleOut(Player player) {	
    	resetEnd(player);
        if (player.world() != 0) {
            TeleportService2.teleportTo(player, player.world(), player.locX(), player.locY(), player.locZ(), (byte) player.locH(), TeleportAnimation.JUMP_AIMATION_3);
        }
        else {
            TeleportService2.moveToBindLocation(player, true);
        }
        player.clearPrevLoc();    
    }       
  
    
    /**
     * TradeKillAlert Reducing When Using Same Mac
     */

	private void checkIfSameMac(Player winner, Player loser){
        String ip1 = winner.getClientConnection().getIP();
        String mac1 = winner.getClientConnection().getMacAddress();
        String ip2 = loser.getClientConnection().getIP();
        String mac2 = loser.getClientConnection().getMacAddress();
        if ((mac1 != null) && (mac2 != null)) {
            if ((ip1.equalsIgnoreCase(ip2)) && (mac1.equalsIgnoreCase(mac2))) {
                AuditLogger.info(winner, "[TradeKillAlert] You really need to check player " + winner.getName() + " and " + loser.getName() + ", They have same IP and MAC and possible they are Trade Killing in FFA, so please go and check in invisible! (MAC: " + mac1 + ").");
                int lose_ap = 30000;
                int lose_gp = 5000;
                int omegaId = 166020000;
                int tsId = 166030005;
                AbyssPointsService.addAp(winner, -lose_ap); // reducing ap from trade killers
                AbyssPointsService.addAp(loser, -lose_ap);

                AbyssPointsService.addGp(winner, -lose_gp); // reducing gp from trade killers
                AbyssPointsService.addGp(loser, -lose_gp);

                winner.getInventory().decreaseByItemId(omegaId, 1); // removing Omega from trade killers
                loser.getInventory().decreaseByItemId(omegaId, 1);

                loser.getInventory().decreaseByItemId(tsId, 1); //removeing TS from trade killers
                winner.getInventory().decreaseByItemId(tsId, 1);
                PacketSendUtility.sendMessage(winner, "[TradeKillAlert] You lost " + lose_ap + " AP for Trade Killing!"); // AP Lose msg
                PacketSendUtility.sendMessage(loser, "[TradeKillAlert] You lost " + lose_ap + " AP for Trade Killing!");
                PacketSendUtility.sendMessage(winner, "[TradeKillAlert] You lost " + lose_gp + " GP for Trade Killing!"); // GP Lose msg
                PacketSendUtility.sendMessage(loser, "[TradeKillAlert] You lost " + lose_gp + " GP for Trade Killing!");
                PacketSendUtility.sendMessage(winner, "[TradeKillAlert] You lost [item:" + omegaId + "] x 1 and [item:" + tsId + "] x 1 item(s) for Trade Killing!"); // item lose Item msg
                PacketSendUtility.sendMessage(loser, "[TradeKillAlert] You lost [item:" + omegaId + "] x 1 and [item:" + tsId + "] x 1 item(s) for Trade Killing!"); // item lose Item msg
                PacketSendUtility.sendMessage(winner, "[TradeKillAlert] Next Time Don't Trade Kill >_>");
                PacketSendUtility.sendMessage(loser, "[TradeKillAlert] Next Time Don't Trade Kill >_>");
                log.info("[FFA-TradeKill] Player " + winner.getName() + " killed " + loser.getName() + " and Have same IP and MAC!");
                return;
            }
            if (ip1.equalsIgnoreCase(ip2)) {
                AuditLogger.info(winner, "[TradeKillAlert] Possible chances that " + winner.getName() + " and " + loser.getName() + " are trade killing in FFA. They have same ip " + ip1 + ".");
                AuditLogger.info(winner, "[TradeKillAlert] If not, they are in some kinda cafe, with same network. OR USING SAME WTFAST Connection!!");
            }
        }
    }
  
    /**
     * On die Section & Rewards
     */
    
 public void onDead(final Player player, Creature lastAttacker) {
      PacketSendUtility.broadcastPacket(player, new SM_EMOTION(player, EmotionType.DIE, 0, player.equals(lastAttacker) ? 0 : lastAttacker.getObjectId()), true);
      PacketSendUtility.sendPacket(player, new SM_DIE(false, false, 0, 8));    	
  	  onReward(player, lastAttacker);
 	}    
	
	public void onRevive(Player player) {      		
		PlayerReviveService.revive(player, 100, 100, false, 0);
		player.getGameStats().updateStatsAndSpeedVisually();
		PacketSendUtility.sendPacket(player, SM_SYSTEM_MESSAGE.STR_REBIRTH_MASSAGE_ME);		
		if (player.isInFFA()) {
			FFaService.getInstance().TeleIn(player); 
		} else {
        	TeleportService2.teleportTo(player, worldNumber, instance.getInstanceId(), (float) loc.x, (float) loc.y, (float) loc.z, (byte) loc.h, TeleportAnimation.JUMP_AIMATION_3);
		}
	}
	 
	public void onReward(Player victim, Creature lastAttacker) {	
    	if (lastAttacker.getActingCreature() instanceof Player && victim != lastAttacker) {    	
    		Player winner;
    		Killer =  victim.getAggroList().getMostPlayerDamage();	
			if (Killer.getLifeStats().isAlreadyDead()) {
				winner = (Player) lastAttacker;	
				checkIfSameMac(winner, victim);
			} else {
				winner = Killer;
			}			
			AbyssPointsService.addAp(winner, 5000);
			AbyssPointsService.addGp(winner, 100);
			winner.setSpecialKills();
			checkIfSameMac(winner, victim);
         	//Send FFA Message die
        	sendSpecMessage("FFA", String.format("%s has slain %s..ouch \uE07A!", winner.getName(), victim.getName()), instance); 
	        throwStreakAnnouncement(winner);
        }
    }	
      
    private void throwStreakAnnouncement(Player winner) {
        if(winner.getSpecialKills() == 5) {        	
            PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " is your new Enemy! +10 Hero Points  (Kills: " + winner.getSpecialKills() +")", ChatType.BRIGHT_YELLOW_CENTER, false); 
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll()); 
        } else if (winner.getSpecialKills() == 10) {
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " is the War Champion! +15 Hero Points (Kills: " + winner.getSpecialKills() +")", ChatType.BRIGHT_YELLOW_CENTER, false); 
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll());  
        } else if (winner.getSpecialKills() == 15) {
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " wants more blood! +20 Hero Points (Kills: " + winner.getSpecialKills() +")", ChatType.BRIGHT_YELLOW_CENTER, false); 
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll());  
        } else if (winner.getSpecialKills() == 20) {
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " is like a crazy monster! +25 Hero Points (Kills: " + winner.getSpecialKills() +")", ChatType.BRIGHT_YELLOW_CENTER, false); 
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll()); 	 
        } else if (winner.getSpecialKills() == 25) {
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " is a Killing Machine! Go Hunt Him Down (+30 HP)", ChatType.BRIGHT_YELLOW_CENTER, false); 
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll()); 	 
        } else if (winner.getSpecialKills() == 30) {
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + ", Are you okay? " + winner.getSpecialKills() + " fucking kills?? (+35 HP)", ChatType.BRIGHT_YELLOW_CENTER, false); 
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll()); 	 
        } else if (winner.getSpecialKills() == 35) {
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll());  
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " is a BOSS, certified KILLER! (+40 HP)", ChatType.BRIGHT_YELLOW_CENTER, false); 
        } else if (winner.getSpecialKills() == 40) {
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll());  
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " is a Hero! (+45 HP)", ChatType.BRIGHT_YELLOW_CENTER, false); 
        } else if (winner.getSpecialKills() == 45) {
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll()); 	 
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " knows the Enemies best! (+50 HP)", ChatType.BRIGHT_YELLOW_CENTER, false); 
        } else if (winner.getSpecialKills() == 50) {
            ItemService.addItem(winner, 166030005, 1);			
			InGameShopEn.getInstance().addToll(winner, 10);
			PacketSendUtility.sendMessage(winner, "You have receive 10 tolls. Totals: "+ winner.getPlayerAccount().getToll());  
        	PacketSendUtility.sendAnnounceMessage("[FFA] "+ winner.getName() + " is the king of FFA.", ChatType.BRIGHT_YELLOW_CENTER, false); 
            TeleIn(winner);
        } 
    }    
    
    private void resetEnd(Player player) {
        player.setInFFA(false);
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
       	log.info(String.format("[FFA] %s Exit - Killed: %s", player.getName(), player.getSpecialKills()));
       	player.setZeroSpecialKills();
       	//PacketSendUtility.sendSpecMessage("FFA", player.getName() + " has left the battle.");
    }
    
    private void resetStart(Player player) {
    	player.setInFFA(true);
    	player.setZeroSpecialKills();
    	InstanceService.registerPlayerWithInstance(instance, player);
    	AddProtection(player, 10 * 1000);
    	if (player.getAccessLevel() > 4) {
			player.setInvul(false);
			player.setAdminNeutral(0);
			player.unsetSeeState(CreatureSeeState.SEARCH10);
		}	
    	HealPlayer(player, false, true);
    }
    
	/**
	 * Display Template For FFA/2v2/Arena
	 */
    
	public static int getDisplayTemplate(Player player, Item item) {
		if (item.getItemTemplate().isWeapon()) {
			switch (item.getItemTemplate().getWeaponType()) {
				case POLEARM_2H: // Gulare's Polearm
					return 101300983;
				case DAGGER_1H: // Gulare's Dagger
					return 100201189;
				case BOW: // Gulare's Bow
					return 101701064;
				case SWORD_1H: // Gulare's Sword
					return 100001348;
				case SWORD_2H: // Gulare's Greatsword
					return 100901040;
				case MACE_1H: // Gulare's Mace
					return 100101027;
				case STAFF_2H: // Gulare's Staff
					return 101501055;
				case ORB_2H: // Gulare's Orb
					return 100501040;
				case BOOK_2H: // Gulare's Spellbook
					return 100601092;
				case GUN_1H: // Gulare's Pistol
					return 101800837;
				case CANNON_2H: // Gulare's Aethercannon
					return 101900841;
				case HARP_2H: // Gulare's Harp
					return 102000875;
				case KEYBLADE_2H: // Gulare's Cipher-Blade
					return 102100733;
				default:
					return 102100733;
			}
		} else if (item.getEquipmentSlot() == 8) {// Torse
			if (player.getRace() == Race.ELYOS) {
				return 110101255;// Legion suit
			} else {
				return 110101257;// Legion suit
			}
		} else if (item.getItemTemplate().getArmorType() == ArmorType.SHIELD) // Shield
		{
			return 115001388;// Antro's Shield
		} else {
			return item.getItemSkinTemplate().getTemplateId();
		}
	}
	
	/**
	 * Get Display Name
	 */
	
	public String getName(Player player, Player target) {
		if (player.isGmMode()) {
			return target.getName();
		}

		String FFAplayerName = target.getPlayerClass().name();

		//if (!player.isInGroup2() && !player.isInAlliance2()) {
		//	FFAplayerName += " " + (target.getFFAIndex() + 1);
		//}

		return FFAplayerName;
	  }
	}
