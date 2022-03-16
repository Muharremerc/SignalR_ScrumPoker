$(document).ready(function(){
	 const api = "https://localhost:44370/api/v1/";
	const connection = "https://localhost:44370/groupHub";
	
	// const connection = "http://90.158.18.82:7075/groupHub";
	// const api = "http://90.158.18.82:7075/api/v1/";
	
	
	const  connectionGroupHub = new signalR.HubConnectionBuilder().withUrl(connection).build();
	var staticConnectionId = "";
	var staticSelectedGroupId = "";
	var staticUserName="";
	connectionGroupHub.on("connectionInfo",connectionId =>
	{
		
	    staticConnectionId = connectionId;
	    getCreatedGroupList();
	    console.log("connectionInfo");
	    console.log(staticConnectionId);
	})
	
	
	connectionGroupHub.on("createdGroup",groupId =>
	{
		
	    console.log("createdGroup");
	    console.log(groupId);
	})
	
	
	connectionGroupHub.on("disconnededUser",user =>
	{
			
		   console.log("disconnededUser");
		   console.log(user);
	})
	
	connectionGroupHub.on("refreshGroup",groupUsers =>
	{
		
		$("#userListDiv").empty();
		groupUsers.userList.forEach(element => 
		{
			
			var tempUser = createUserDiv(element);
			$("#userListDiv").append(tempUser);
		});
	    console.log("refreshGroup");
	    console.log(groupUsers);
	})
	


	connectionGroupHub.on("appedMessage",messageDetail =>
	{
		debugger;
			appedChatMessage(messageDetail);
		
	})

	connectionGroupHub.onclose(e => 
	{   
		
        $("#btnConnect").show();
    });

	connectionGroupHub.on("groupList",groupList =>
	{
		
		$("#groupsListDiv").empty();
		groupList.groups.forEach(element => 
		{
			var tempGroup = createGroupDiv(element);
			$("#groupsListDiv").append(tempGroup);
		});
	})


	$("#btnEnter").click(()=>{
		var loginDiv = document.getElementById("loginDiv");
		var mainDiv = document.getElementById("mainDiv");
		var userNameDiv = document.getElementById("userNameDiv");
		connectionGroupHub.start().then(() => 
		{
			loginDiv.style.display="none";
			mainDiv.style.display="block";
			userNameDiv.style.display="block";
			staticUserName = $("#inputName").val();
			debugger;
			$("#userNameDiv").html(staticUserName);
		})
		.catch(function (err) 
		{
			console.log("Error"+err)
		});

	});

	
	$(".point").click(function(){
		var point = $(this).attr("value");
		givepoint(point);
	})

	///Group Methods
	$("#btnGroupCreate").click(()=>
	{
		creatgroup($("#inputGroupName").val());
	})

	

	function setGroupName(groupName)
	{ 
		
		 $("#userHeaderDiv").html(groupName);
	}

	function createGroupDiv(groupDetail) 
	{   
		
		let div = document.createElement('div');
		div.className = 'groupDiv';
		var customAttr = document.createAttribute('group_data');
		customAttr.value = groupDetail.groupId
		div.onclick= groupClick;
		div.setAttributeNode(customAttr);
		div.appendChild(createGroupNameDiv(groupDetail.name));
		div.appendChild(createGroupCountDiv(groupDetail.memberCount));
		return div;
	}
	function createGroupNameDiv(name) 
	{
		let div = document.createElement('div');
		div.className = 'groupNameDiv';
		div.innerHTML = name;
		return div;
	}
	function createGroupCountDiv(count) 
	{
		let div = document.createElement('div');
		div.className = 'groupCountDiv';
		div.innerHTML = count;
		return div;
	}
	function groupClick(){

		staticSelectedGroupId = $(this).attr("group_data");
		var selectedGroupName=this.children[0].innerText;
		setGroupName(selectedGroupName);
		join();
		getChat();
   };
   



	///Append User

	function createUserDiv(userDetail) 
	{
		let div = document.createElement('div');
		div.className = 'userDiv';
		if(userDetail.isDisconnected == true)
		{div.style.color = 'red';}

		div.appendChild(createUserNameDiv(userDetail.name));
		div.appendChild(createUserPointDiv(userDetail.point));
		return div;
	}


	function createUserNameDiv(name) 
	{
		let div = document.createElement('div');
		div.className = "userNameDiv"
		div.innerHTML = name;
		return div;
	}
	

	
	function createUserPointDiv(point) 
	{
		let div = document.createElement('div');
		div.className = "userPointDiv";
		div.innerHTML = point;
		return div;
	}
	///Chat
     
	function createChatDetail(chatDetail)
	{
		let div = document.createElement('div');
		div.className = 'chatMessageDetail';

		debugger;
		div.appendChild(createChatSender(chatDetail.sender));
		div.appendChild(createChatDate(chatDetail.date));
		div.appendChild(createChatMessage(chatDetail.message));
		debugger
		return div;
	}

	function createChatSender(sender)
	{
		debugger
		let div = document.createElement('div');
		div.className = "chatSender";
		div.innerHTML = sender;
		return div;
	}
	function createChatDate(sender)
	{debugger
		let div = document.createElement('div');
		div.className = "chatDate";
		div.innerHTML = sender;
		return div;
	}
	function createChatMessage(sender)
	{debugger
		let div = document.createElement('div');
		div.className = "chatMessage";
		div.innerHTML = sender;
		return div;
	}

	///

	
	$('#btnClear').click(()=>{

		let request = JSON.stringify({
			"groupId": staticSelectedGroupId,
			"connectionId": staticConnectionId
			});
			$.ajax({
				url: api+"Group/Clear",
				type: 'PUT',
				dataType: 'json',
				contentType: "application/json; charset=utf-8",
				data: request,
				success: function (response) {
						
				},
				error: function (hata) {
		 
				}
			});
		})

	$("#btnHide").click(()=>{
	
		let request = JSON.stringify({
			"groupId": staticSelectedGroupId,
			"connectionId": staticConnectionId
			});
			$.ajax({
				url: api+"Group/Hide",
				type: 'PUT',
				dataType: 'json',
				contentType: "application/json; charset=utf-8",
				data: request,
				success: function (response) {
						
				},
				error: function (hata) {
		 
				}
			});
		})

		
		function getCreatedGroupList()
		{
			$("#groupsListDiv").empty();
			
			$.ajax({
				url: api+"Group",
				type: 'GET',
				dataType: 'json',
				success: function (response) {
				
					response.data.groups.forEach(element => 
					{
						
						var tempGroup = createGroupDiv(element);
						$("#groupsListDiv").append(tempGroup);
					});
				},
				error: function (hata) {
					
				}
			});
		}
	 
		function join()
		{
			let request = JSON.stringify({
			"connectionId":staticConnectionId,
			"groupId": staticSelectedGroupId,
			"userName": staticUserName
			});
			
			$.ajax({
				url: api+"Group/Join",
				type: 'POST',
				dataType: 'json',
				contentType: "application/json; charset=utf-8",
				data: request,
				success: function (response) {
						
				},
				error: function (hata) {
		 
				}
			});
		}
	 
		function givepoint(selectedPoint)
		{ 
		let request = JSON.stringify({
			"connectionId": staticConnectionId,
			"groupId": staticSelectedGroupId,
			"point": selectedPoint
		});
			$.ajax({
				url: api+"Point/Give",
				type: 'POST',
				dataType: 'json',
				contentType: "application/json; charset=utf-8",
				data: request,
				success: function (response) {
				},
				error: function (hata) {
				}
			});
		}

	function creatgroup(groupName)
	{
	    var that = this;
		let request = JSON.stringify({
        "connectionId": staticConnectionId,
        "groupName": groupName,
        "userName": staticUserName
    });
		$.ajax({
			url: api+"Group/Create",
			type: 'POST',
			dataType: 'json',
			contentType: "application/json; charset=utf-8",
			data: request,
			success: function (response) {
				staticSelectedGroupId=response.data.groupId;
				setGroupName(groupName);
				getChat();
			},
			error: function (hata) {
	 
			}
		});
	}

	function getChat()
	{
		$.ajax({
			url: api+"Chat/"+staticSelectedGroupId+"/"+staticConnectionId,
			type: 'Get',
			dataType: 'json',
			contentType: "application/json; charset=utf-8",
			success: function (response) {
				loadChat(response)
			},
			error: function (hata) {
	 
			}
		});
	}

	function chatSendMessage(message)
	{
		var that = this;
		let request = JSON.stringify({
        "groupId": staticSelectedGroupId ,
        "connectionId": staticConnectionId,
        "message": message
    });
		$.ajax({
			url: api+"Chat/Add",
			type: 'POST',
			dataType: 'json',
			contentType: "application/json; charset=utf-8",
			data: request,
			success: function (response) {
			},
			error: function (hata) {
	 
			}
		});
	}
	$("#btnSend").click(()=>{

		var message = $("#inputMessage").val();
		chatSendMessage(message);
	})
	function loadChat(chatDetail)
	{
		$("#chatDiv").empty();
		debugger;
		var messageLength =chatDetail.data.messageDetailList.length;
		debugger;
		if(messageLength != 0)
		{
		chatDetail.data.messageDetailList.forEach(message => 
			{
				debugger;
				appedChatMessage(message);
			});
		}	
	}
	function appedChatMessage(message)
	{
		debugger;
		var messageDiv = createChatDetail(message);
		debugger;
		$("#chatDiv").prepend(messageDiv);
	}
	 
});
