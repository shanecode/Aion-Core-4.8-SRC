package com.aionemu.gameserver.services.ecfunctions;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.aionemu.commons.network.util.ThreadPoolManager;
import com.aionemu.gameserver.ai2.AI2Engine;
import com.aionemu.gameserver.ai2.AIState;
import com.aionemu.gameserver.ai2.AbstractAI;
import com.aionemu.gameserver.instance.handlers.GeneralInstanceHandler;
import com.aionemu.gameserver.model.ChatType;
import com.aionemu.gameserver.model.Race;
import com.aionemu.gameserver.model.TeleportAnimation;
import com.aionemu.gameserver.model.gameobjects.Creature;
import com.aionemu.gameserver.model.gameobjects.Npc;
import com.aionemu.gameserver.model.gameobjects.StaticDoor;
import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.model.templates.spawns.SpawnTemplate;
import com.aionemu.gameserver.model.utils3d.FFA3D;
import com.aionemu.gameserver.network.aion.serverpackets.SM_TRANSFORM;
import com.aionemu.gameserver.services.teleport.TeleportService2;
import com.aionemu.gameserver.skillengine.SkillEngine;
import com.aionemu.gameserver.skillengine.effect.AbnormalState;
import com.aionemu.gameserver.skillengine.effect.EffectTemplate;
import com.aionemu.gameserver.skillengine.model.Effect;
import com.aionemu.gameserver.skillengine.model.Skill;
import com.aionemu.gameserver.skillengine.model.SkillTargetSlot;
import com.aionemu.gameserver.spawnengine.SpawnEngine;
import com.aionemu.gameserver.utils.PacketSendUtility;
import com.aionemu.gameserver.world.WorldMapInstance;


/**
 * @author Goong ADM
 *
 */
public class PVPManager extends GeneralInstanceHandler {

	
    private final Logger log = LoggerFactory.getLogger(PVPManager.class);
    
    private static final PVPManager service = new PVPManager();
	
	private TeleportAnimation Tele = null; 
	private int count;
	private String winnerName, loserName;

	
	private int worldID;
	private double ax, ay, az, ah, ax1, ay1, az1, ah1;	
	private double ex, ey, ez, eh, ex1, ey1, ez1, eh1;
    
    public static PVPManager getInstance(){
        return service;
    }
    public void Init() {	
    	setSiegeMAP();
    	spawnPortals();
    }
    private void setSiegeMAP() {  	
		ax = 236.55055; //Kaldor
		ay = 522.90045;
		az = 202.09566;
		ah = 0;
		
		ex = 1371.0321;
		ey = 533.8006;
		ez = 276.4394;
		eh = 0;
		worldID = 600090000;
		
    }
    
    public void TeleSiege (Player player) {	
    	if (player.getRace() == Race.ASMODIANS) {
    		TeleportService2.teleportTo(player, worldID, (float) ax, (float) ay, (float) az, (byte) ah);
    	} else {
    		TeleportService2.teleportTo(player, worldID, (float) ex, (float) ey, (float) ez, (byte) eh);
    	}	
    }   
    public int getWorldID () {
    	return worldID;
    }  
	private void spawnPortals() {
		/*
		 * Siege Loc in Panda/Sanctum
		 */
        SpawnTemplate spawnElySiege = SpawnEngine.addNewSpawn(110010000, 730662, 1499, 1492, 565, (byte) 58, 0);
        spawnElySiege.setMasterName("PVP-SIEGE");
        SpawnEngine.spawnObject(spawnElySiege, 1);
        AI2Engine.getInstance().setupAI("pvp_portal", (Creature) spawnElySiege.getVisibleObject());
        ((AbstractAI) ((Creature) spawnElySiege.getVisibleObject()).getAi2()).setStateIfNot(AIState.IDLE);

        SpawnTemplate spawnAsmoSiege = SpawnEngine.addNewSpawn(120010000, 730662, 1353, 1367, 208, (byte) 59, 0);
        spawnAsmoSiege.setMasterName("PVP-SIEGE");
        SpawnEngine.spawnObject(spawnAsmoSiege, 1);
        AI2Engine.getInstance().setupAI("pvp_portal", (Creature) spawnAsmoSiege.getVisibleObject());
        ((AbstractAI) ((Creature) spawnAsmoSiege.getVisibleObject()).getAi2()).setStateIfNot(AIState.IDLE);        
	}
	
    /*
     * Misc features
     */
    public String toLowercase (String text) {
        String theText = text;
        theText = theText.toLowerCase();
        theText = Character.toString(theText.charAt(0)).toUpperCase() + theText.substring(1);
        return theText;
    }
    public void openDoors(WorldMapInstance instance) {
        for (StaticDoor door : instance.getDoors().values()) {
            if (door != null) {
            	door.setOpen(true);
            }
        }
    }    
	protected FFA3D spawnLoc(FFA3D loc, FFA3D [] spawnArray, int position) {
    	loc = spawnArray[position];
    	return loc;
    }	
    protected void xMorph(Player target) {
        target.getTransformModel().setModelId(0);
        PacketSendUtility.broadcastPacketAndReceive(target, new SM_TRANSFORM(target, true));
    }
    protected String getWinnerName () {
    	return winnerName;
    }
    protected String getLoserName () {
    	return loserName;
    } 
    /*
     * Root/Paralize/Sleep
     */
    
    public void paralizePlayer(Player player, boolean canParalize) {
    	if (canParalize) {
    		player.getEffectController().setAbnormal(AbnormalState.PARALYZE.getId());
    	} else {
    		player.getEffectController().unsetAbnormal(AbnormalState.PARALYZE.getId());
    	}
    	player.getEffectController().updatePlayerEffectIcons();
    	player.getEffectController().broadCastEffects();
    }    
    public void rootPlayer(Player player, boolean canRoot) {
    	if (canRoot) {
    		player.getEffectController().setAbnormal(AbnormalState.ROOT.getId());
    	} else {
    		player.getEffectController().unsetAbnormal(AbnormalState.ROOT.getId());
    	}
    	player.getEffectController().updatePlayerEffectIcons();
    	player.getEffectController().broadCastEffects();
    }
    public int getPlayerCountDFFA(WorldMapInstance instance) {
        if(instance == null) {
            return 0;
        } else {
        	count = 0;
			for (Player p : instance.getPlayersInside()) {
				if (p.isInDuoFFA()) {
					count++;
				}
			}
			return count;
        }   
    }    
    public int getPlayerCountFFA(WorldMapInstance instance) {
        if(instance == null) {
            return 0;
        } else {
        	count = 0;
			for (Player p : instance.getPlayersInside()) {
				if (p.isInFFA()) {
					count++;
				}
			}
			return count;
        }   
    }    
    public void AddProtection(final Player p, int duration) {    	
        Skill protector1 = SkillEngine.getInstance().getSkill(p, 9833, 1, p.getTarget());
        Skill protector2 = SkillEngine.getInstance().getSkill(p, 18474, 1, p.getTarget());
        
        Effect a = new Effect(p, p, protector1.getSkillTemplate(), protector1.getSkillLevel(), duration);
        Effect b = new Effect(p, p, protector2.getSkillTemplate(), protector2.getSkillLevel(), duration);
        
        for (EffectTemplate at : a.getEffectTemplates()) {
            at.setDuration(duration);
        }
        for (EffectTemplate bt : b.getEffectTemplates()) {
        	bt.setDuration(duration);
        }
        a.initialize();
        a.applyEffect();
        b.initialize();
        b.applyEffect();
        ThreadPoolManager.getInstance().schedule(new Runnable() {
            @Override
            public void run() {
            	p.getEffectController().removeEffect(9833);
            	p.getEffectController().removeEffect(18474);
            }
        }, duration);
    }
    protected void HealPlayer(Player player, boolean withDp, boolean sendUpdatePacket) {
        player.getLifeStats().setCurrentHpPercent(100);
        player.getLifeStats().setCurrentMpPercent(100);
        if (withDp) {
            player.getCommonData().setDp(4000);
        }
        if (sendUpdatePacket) {
            player.getLifeStats().sendHpPacketUpdate();
            player.getLifeStats().sendMpPacketUpdate();
        }
        player.getEffectController().removeAbnormalEffectsByTargetSlot(SkillTargetSlot.DEBUFF);
    }
    /*
     * Send Message Holder     
     */    
    public void sendSpecMessage(String sender, String msg, WorldMapInstance instance) {
        for (Player p : instance.getPlayersInside()) {
            PacketSendUtility.sendMessage(p, sender, msg, ChatType.GROUP_LEADER);
        }
    }
    public void sendSpecMessageHolder(String sender, String msg, List<Player> thePlayers) {
		for (Player p : thePlayers) {
			PacketSendUtility.sendMessage(p, sender, msg, ChatType.GROUP_LEADER);
		}
	}
	protected void deSpawnPortal(SpawnTemplate spawn) {
        Npc npcSpawn = null;
        npcSpawn = (Npc) spawn.getVisibleObject();
        if (npcSpawn != null) {
        	npcSpawn.getController().onDelete();
        }
	}
}
