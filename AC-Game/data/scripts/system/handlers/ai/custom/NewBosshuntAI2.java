package ai.custom;

import com.aionemu.gameserver.ai2.AIName;
import com.aionemu.gameserver.model.gameobjects.Creature;
import com.aionemu.gameserver.model.gameobjects.Npc;
import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.model.gameobjects.player.RequestResponseHandler;
import com.aionemu.gameserver.model.team2.group.PlayerGroup;
import com.aionemu.gameserver.network.aion.serverpackets.SM_QUESTION_WINDOW;
import com.aionemu.gameserver.services.ecfunctions.bosshunt.BHService;
import com.aionemu.gameserver.utils.PacketSendUtility;

import ai.ActionItemNpcAI2;

/**
 *
 * @author Ghostfur
 */
@AIName("bosshunt_portal")
public class NewBosshuntAI2 extends ActionItemNpcAI2 {   
    @Override
    protected void handleDialogStart(final Player player) {
        Npc owner = getOwner();        
        int memberBoss = 4;
        if (owner.getMasterName().equalsIgnoreCase("BOSSHUNT-" + BHService.getInstance().worldName)) {
            if (player.getController().isInCombat() 
        		|| player.isInPkMode() 
        		|| player.isInArena() 
        		|| player.isInFFA() 
				|| player.isInDuoFFA() 
        		|| player.isInPrison() 
        		|| player.getLifeStats().isAlreadyDead()) {
	            PacketSendUtility.sendMessage(player, "You can not participate in Bosshunt, while dead/in prison/in combat/in another battleground.");
	            return;
            }       
            if (player.getAccessLevel() > 0 && player.getAccessLevel() <= 5) {
            	PacketSendUtility.sendMessage(player, "GM cannot join in bosshunt. Log in normal account.");
            	return;
            }   

            if (player.isInAlliance2()){
            	PacketSendUtility.sendMessage(player, "You must have group and be leader.");
            	return;
            }  
            
            if (!player.isInGroup2()) {
            	PacketSendUtility.sendMessage(player, "Only group allow to joining bosshunt.");
            	return;
            }
            
            PlayerGroup group = player.getPlayerGroup2();

            if (group.size() > 4) {
	            PacketSendUtility.sendMessage(player,"Group size is limited to 4 members.");
	            return;	 
            }
            RequestResponseHandler responseHandler = new RequestResponseHandler(player) {
                @Override
                public void acceptRequest(Creature p2, Player p) {
                    start(p);
                }
                @Override
                public void denyRequest(Creature p2, Player p) {
                }
            };
            boolean requested = player.getResponseRequester().putRequest(902247, responseHandler);
            if (requested) {
                PacketSendUtility.sendPacket(player, new SM_QUESTION_WINDOW(902247, 0, 0,  "testing"));
            
            }            
            return;
        }
        super.handleDialogStart(player);
    }

    private void start(Player player) {
        super.handleDialogStart(player);
    }

    @Override
    protected void handleUseItemFinish(Player player) {
      Npc owner = getOwner();        
      if (owner.getMasterName().equalsIgnoreCase("BOSSHUNT-" + BHService.getInstance().worldName)) {
    	  PlayerGroup group = player.getPlayerGroup2();        	
      	for (Player member : group.getMembers()) {    
	        	member.setPrevLoc();        
    		}        	
        	BHService.getInstance().TeleIn(player);           	
       }
       super.handleUseItemFinish(player);
    }    

}
