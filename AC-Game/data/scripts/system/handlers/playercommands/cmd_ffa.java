package playercommands;

import java.util.concurrent.TimeUnit;

import com.aionemu.commons.network.util.ThreadPoolManager;
import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.model.team2.group.PlayerGroupService;
import com.aionemu.gameserver.network.aion.serverpackets.SM_ITEM_USAGE_ANIMATION;
import com.aionemu.gameserver.services.ecfunctions.PVPManager;
import com.aionemu.gameserver.services.ecfunctions.ffa.FFaService;
import com.aionemu.gameserver.utils.PacketSendUtility;
import com.aionemu.gameserver.utils.chathandlers.PlayerCommand;

/**
 * @author Ghostfur
 */
public class cmd_ffa extends PlayerCommand {
    
    public cmd_ffa() {
        super("ffa");
    }

    @Override
    public void execute(final Player player, String... params) {
       if (player.isInFFA()) {
        	if (player.getLifeStats().isAlreadyDead()) {
        		PacketSendUtility.sendMessage(player, "FFA: You cannot use this command while dead.");
        		return;
        	}
        	PacketSendUtility.sendMessage(player, "FFA: Please wait 5 seconds.");
        	PVPManager.getInstance().paralizePlayer(player, true);
        	PlayerGroupService.removePlayer(player); //remove player from group in FFA
            PacketSendUtility.sendPacket(player, new SM_ITEM_USAGE_ANIMATION(player.getObjectId(), 0, 0, (int) TimeUnit.SECONDS.toMillis(5), 0, 0));
            ThreadPoolManager.getInstance().schedule(new Runnable() {
                @Override
                public void run() {
                	PVPManager.getInstance().paralizePlayer(player, false);
                    PacketSendUtility.broadcastPacketAndReceive(player, new SM_ITEM_USAGE_ANIMATION(player.getObjectId(), 0, 0, 0, 1, 0));
                    FFaService.getInstance().TeleOut(player);  
                }
            }, (int) TimeUnit.SECONDS.toMillis(5));
       }
    }
    @Override
    public void onFail(Player player, String message) {
        PacketSendUtility.sendMessage(player, "Syntax: Type '.ffa' to exit.");
    }
}

