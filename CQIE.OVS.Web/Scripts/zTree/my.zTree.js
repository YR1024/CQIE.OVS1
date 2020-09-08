//�����ƶ��ڵ㵽��һ�����Ķ�Ӧ�Ľڵ㣬����targetNode���Ϊnull���ƶ���2�������ṹ������ͬ�����ƶ���Ľڵ㱣��һ�£�����Ŀ��ڵ��Ϊ����Ľڵ�
function MoveNodes(nodeList, sourceTree, targetTree, targetNode) {
    for (var i = 0; i < nodeList.length; i++) {
        AddNode(nodeList[i], sourceTree, targetTree, targetNode);
        if (targetNode != null) RemoveNodeList(nodeList[i], sourceTree); //�Ƴ�ѡ��ڵ�
        else if (i > 0) RemoveNodeList(nodeList[i], sourceTree); //���Ƴ����ڵ�
    }
}


//���ӽڵ�
function AddNode(node, sourceTree, targetTree, targetNode) {
    var parentNode;
    //������ǰ����Ӧ�Ľڵ�
    var tempNode = targetTree.getNodesByParam("id", node.id, null); //����һ���������Ƿ����
    if (tempNode[0] != null) return; //�ڵ���ڣ�����

    if (targetNode == null) {        
        parentNode = targetTree.getNodesByParam("id", node.pId, null)[0]; //���Ҹ��ڵ�
    }
    else {
        //ֱ��ָ�����ڵ�
        parentNode = targetNode;
    }
    var newNode = { id: node.id, pId: node.pid, name: node.name, isParent: node.isParent, open: node.open, checked: false };
    targetTree.addNodes(parentNode, newNode); //��Ŀ��ڵ�������
}

//�Ƴ��ڵ㣨���ڵ���⣩
function RemoveNodeList(node, sourceTree) {
    if (!node.isParent) sourceTree.removeNode(node); //Ҷ�ӽڵ㣬ֱ���Ƴ�
    else if (CanRmove(node)) {
        sourceTree.removeNode(node); //�Ƴ��ڵ�
    }
}
//�Ƿ����ƶ�������ӽڵ�ȫ��ѡ�У�������Ƴ�
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

//��ȡ�������нڵ�
function GetAllNodes(treeId) {
    var zTreeSource = $.fn.zTree.getZTreeObj(treeId);
    var idList = "";
    var nch = zTreeSource.getCheckedNodes(false); //δѡ�еĽڵ�
    if (nch != null) {
        $(nch).each(function (i, item) {
            if (item.id > 0) {
                if (idList != "") idList += ","
                idList += item.id;
            }
        });
    }
    nch = zTreeSource.getCheckedNodes(true); //ѡ�еĽڵ�
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

//��ȡ�����ӵĽڵ�
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

//��ȡ��ɾ���Ľڵ�
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

