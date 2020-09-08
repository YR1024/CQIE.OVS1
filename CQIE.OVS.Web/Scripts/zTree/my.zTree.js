//批量移动节点到另一棵树的对应的节点，参数targetNode如果为null，移动的2个棵树结构必须相同，且移动后的节点保持一致，否则目标节点可为任意的节点
function MoveNodes(nodeList, sourceTree, targetTree, targetNode) {
    for (var i = 0; i < nodeList.length; i++) {
        AddNode(nodeList[i], sourceTree, targetTree, targetNode);
        if (targetNode != null) RemoveNodeList(nodeList[i], sourceTree); //移除选择节点
        else if (i > 0) RemoveNodeList(nodeList[i], sourceTree); //不移除跟节点
    }
}


//增加节点
function AddNode(node, sourceTree, targetTree, targetNode) {
    var parentNode;
    //查找与前树对应的节点
    var tempNode = targetTree.getNodesByParam("id", node.id, null); //在另一颗树查找是否存在
    if (tempNode[0] != null) return; //节点存在，返回

    if (targetNode == null) {        
        parentNode = targetTree.getNodesByParam("id", node.pId, null)[0]; //查找父节点
    }
    else {
        //直接指定父节点
        parentNode = targetNode;
    }
    var newNode = { id: node.id, pId: node.pid, name: node.name, isParent: node.isParent, open: node.open, checked: false };
    targetTree.addNodes(parentNode, newNode); //在目标节点下增加
}

//移除节点（根节点除外）
function RemoveNodeList(node, sourceTree) {
    if (!node.isParent) sourceTree.removeNode(node); //叶子节点，直接移除
    else if (CanRmove(node)) {
        sourceTree.removeNode(node); //移除节点
    }
}
//是否能移动，如果子节点全部选中，则可以移除
function CanRmove(node) {
    if (node == null || node.children == null || node.children.length == 0) return true;
    var rv = true;
    var childNum = node.children.length;
    for (var i = 0; i < childNum; i++) {
        if (!node.children[i].checked)
        { 
            rv = false;break;
        }
        else
        {
            rv = CanRmove(node.children[i]);
            if (!rv) break;
        }
    }
    return rv;
}

//获取树的所有节点
function GetAllNodes(treeId) {
    var zTreeSource = $.fn.zTree.getZTreeObj(treeId);
    var idList = "";
    var nch = zTreeSource.getCheckedNodes(false); //未选中的节点
    if (nch != null) {
        $(nch).each(function (i, item) {
            if (item.id > 0) {
                if (idList != "") idList += ","
                idList += item.id;
            }
        });
    }
    nch = zTreeSource.getCheckedNodes(true); //选中的节点
    if (nch != null) {
        $(nch).each(function (i, item) {
            if (item.id > 0) {
                if (idList != "") idList += ","
                idList += item.id;
            }
        });
    }
    return idList;
}

//获取可增加的节点
function GetAddNodes(nodes) {
    var idList = "";
    if (nodes == null || nodes == undefined || nodes.length == 0) return idList;
    $(nodes).each(function (i, item) {
        if (item.id > 0) {
            if (idList != "") idList += ","
            idList += item.id;
        }
    });
    return idList;
}

//获取可删除的节点
function GetDeleteNodes(nodes) {
    var idList = "";
    if (nodes == null || nodes == undefined || nodes.length == 0) return idList;
    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].id == 0) continue;
        if (CanRmove(nodes[i])) {
            if (idList != "") idList += ","
            idList += nodes[i].id;
        }
    }
    return idList;
}

