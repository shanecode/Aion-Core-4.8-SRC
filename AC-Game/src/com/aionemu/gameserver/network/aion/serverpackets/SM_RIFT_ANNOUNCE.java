/**
 * This file is part of Aion-Lightning <aion-lightning.org>.
 *
 *  Aion-Lightning is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Aion-Lightning is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details. *
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Aion-Lightning.
 *  If not, see <http://www.gnu.org/licenses/>.
 *
 *
 * Credits goes to all Open Source Core Developer Groups listed below
 * Please do not change here something, ragarding the developer credits, except the "developed by XXXX".
 * Even if you edit a lot of files in this source, you still have no rights to call it as "your Core".
 * Everybody knows that this Emulator Core was developed by Aion Lightning 

 * @-Aion-Lightning
 * @Goong_ADM

 

 */
package com.aionemu.gameserver.network.aion.serverpackets;

import javolution.util.FastMap;

import com.aionemu.gameserver.controllers.RVController;
import com.aionemu.gameserver.network.PacketLoggerService;
import com.aionemu.gameserver.network.aion.AionConnection;
import com.aionemu.gameserver.network.aion.AionServerPacket;

/**
 * @author Sweetkr
 * @modified -Enomine-
 */
public class SM_RIFT_ANNOUNCE extends AionServerPacket {

    private int actionId;
    private RVController rift;
    private FastMap<Integer, Integer> rifts;
    private int objectId;
    private int gelkmaros, inggison;

    /**
     * Rift announce packet
     *
     * @param player
     */
    public SM_RIFT_ANNOUNCE(FastMap<Integer, Integer> rifts) {
        this.actionId = 0;
        this.rifts = rifts;
    }

    public SM_RIFT_ANNOUNCE(boolean gelkmaros, boolean inggison) {
        this.gelkmaros = gelkmaros ? 1 : 0;
        this.inggison = inggison ? 1 : 0;
        this.actionId = 1;
    }

    /**
     * Rift announce packet
     *
     * @param player
     */
    public SM_RIFT_ANNOUNCE(RVController rift, boolean isMaster) {
        this.rift = rift;
        this.actionId = isMaster ? 2 : 3;
    }

    /**
     * Rift despawn
     *
     * @param objectId
     */
    public SM_RIFT_ANNOUNCE(int objectId) {
        this.objectId = objectId;
        this.actionId = 4;
    }

    /**
     * {@inheritDoc}
     */
    @Override
    protected void writeImpl(AionConnection con) {
    	PacketLoggerService.getInstance().logPacketSM(this.getPacketName());
        switch (actionId) {
            case 0: //announce
                writeH(0x21);//4.7 // old -->writeH(0x19); // 0x19
                writeC(actionId);
                for (int value : rifts.values()) {
                    writeD(value);
                }
                break;
            case 1:
                writeH(0x09); // 0x09
                writeC(actionId);
                writeD(gelkmaros);
                writeD(inggison);
                break;
            case 2:
                writeH(0x23); // 0x23 
                writeC(actionId);
                writeD(rift.getOwner().getObjectId());
                writeD(rift.getMaxEntries());
                writeD(rift.getRemainTime());
                writeD(rift.getMinLevel());
                writeD(rift.getMaxLevel());
                writeF(rift.getOwner().getX());
                writeF(rift.getOwner().getY());
                writeF(rift.getOwner().getZ());
                writeC(rift.isVortex() ? 1 : 0); // red | blue
                writeC(rift.isMaster() ? 1 : 0); // display | hide
                break;
            case 3:
                writeH(0x0f); // 0x0f
                writeC(actionId);
                writeD(rift.getOwner().getObjectId());
                writeD(rift.getUsedEntries());
                writeD(rift.getRemainTime());
                writeC(rift.isVortex() ? 1 : 0);
                writeC(0); // unk
                break;
            case 4:
                writeH(0x05);
                writeC(actionId);
                writeD(objectId);
                break;

        }
    }
}