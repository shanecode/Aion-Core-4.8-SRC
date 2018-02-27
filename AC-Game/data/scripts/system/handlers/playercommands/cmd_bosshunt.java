/*
 * This file is part of aion-engine <aion-engine.net>
 *
 * aion-engine is private software: you can redistribute it and or modify
 * it under the terms of the GNU Lesser Public License as published by
 * the Private Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * aion-engine is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser Public License for more details.
 *
 * You should have received a copy of the GNU Lesser Public License
 * along with aion-engine.  If not, see <http://www.gnu.org/licenses/>.
 */
package playercommands;

import java.util.concurrent.TimeUnit;

import com.aionemu.commons.network.util.ThreadPoolManager;
import com.aionemu.gameserver.model.gameobjects.player.Player;
import com.aionemu.gameserver.model.team2.group.PlayerGroupService;
import com.aionemu.gameserver.network.aion.serverpackets.SM_ITEM_USAGE_ANIMATION;
import com.aionemu.gameserver.services.ecfunctions.PVPManager;
import com.aionemu.gameserver.services.ecfunctions.bosshunt.BHService;
import com.aionemu.gameserver.utils.PacketSendUtility;
import com.aionemu.gameserver.utils.chathandlers.PlayerCommand;

/**
 * @author Ghostfur
 */
public class cmd_bosshunt extends PlayerCommand {
    
    public cmd_bosshunt() {
        super("bosshunt");
    }

    @Override
    public void execute(final Player player, String... params) {
       if (player.isInBH()) {
        	if (player.getLifeStats().isAlreadyDead()) {
        		PacketSendUtility.sendMessage(player, "BOSSHUNT: You cannot use this command while dead.");
        		return;
        	}
        	PacketSendUtility.sendMessage(player, "BOSSHUNT: Please wait 5 seconds.");
        	PVPManager.getInstance().paralizePlayer(player, true);
        	PlayerGroupService.removePlayer(player); //remove player from group in bosshunt
            PacketSendUtility.sendPacket(player, new SM_ITEM_USAGE_ANIMATION(player.getObjectId(), 0, 0, (int) TimeUnit.SECONDS.toMillis(5), 0, 0));
            ThreadPoolManager.getInstance().schedule(new Runnable() {
                @Override
                public void run() {
                	PVPManager.getInstance().paralizePlayer(player, false);
                    PacketSendUtility.broadcastPacketAndReceive(player, new SM_ITEM_USAGE_ANIMATION(player.getObjectId(), 0, 0, 0, 1, 0));
                    BHService.getInstance().TeleOut(player);  
                }
            }, (int) TimeUnit.SECONDS.toMillis(5));
       }
    }
    @Override
    public void onFail(Player player, String message) {
        PacketSendUtility.sendMessage(player, "Syntax: Type '.bosshunt' to exit.");
    }
}

