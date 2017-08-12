/*
Urdu Keyboard is copyright (c) 2009 Urdu.ca
*/

function changeKey (textControl, evt, keyChecker) {
  var keyCode = evt.keyCode ? evt.keyCode :
                evt.charCode ? evt.charCode :
		evt.which ? evt.which : void 0;
  var key;
  if (keyCode) {
    key = String.fromCharCode(keyCode);
  }
  var keyCheck = keyChecker(keyCode, key);
console.log(keyCheck);
  if (keyCode && window.event && !window.opera && !window.chrome) {
    if (keyCheck.cancelKey) {
      return false;
    }
    else if (keyCheck.replaceKey) {
      window.event.keyCode = keyCheck.newKeyCode;
      if (window.event.preventDefault) {
        window.event.preventDefault();
      }
      return true;
    }
    else {
      return true;
    }
  }
  else if (typeof textControl.setSelectionRange != 'undefined') {
    if (keyCheck.cancelKey) {
      if (evt.preventDefault) {
        evt.preventDefault();
      }
      return false;
    }
    else if (keyCheck.replaceKey) {
// cancel the key event and insert the newKey for the current selection
      if (evt.preventDefault) {
	  evt.preventDefault();
      }
      var oldSelectionStart = textControl.selectionStart;
      var oldSelectionEnd = textControl.selectionEnd;
      var selectedText = textControl.value.substring(oldSelectionStart,
oldSelectionEnd);
      var newText = typeof keyCheck.newKey != 'undefined' ?
keyCheck.newKey : String.fromCharCode(keyCheck.newKeyCode);
      textControl.value = 
        textControl.value.substring(0, oldSelectionStart) +
        newText +
        textControl.value.substring(oldSelectionEnd);
      textControl.setSelectionRange(oldSelectionStart + newText.length,
oldSelectionStart + newText.length);
      return false;
    }
    else {
      return true;
    } 
  }
  else if (keyCheck.cancelKey) {
    if (evt.preventDefault) {
      evt.preventDefault();
    }
    return false;
  }
  else {
    return true;
  }
}
   
function typeUrdu (keyCode, key) {
  var asciiUmlauts = {
	" ": " ", //
	"!": "!", //
	":": ":"  //
  };
  if (" !:".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: asciiUmlauts[key].charCodeAt(), newKey:
asciiUmlauts[key] };
  }
  if ("?".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1567).charCodeAt(), newKey:
String.fromCharCode(1567) };	  
  }
  if ("a".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1575).charCodeAt(), newKey:
String.fromCharCode(1575) };	  
  }
  if ("+".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1570).charCodeAt(), newKey:
String.fromCharCode(1570) };	  
  }
  if ("A".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1619).charCodeAt(), newKey:
String.fromCharCode(1619) };	  
  }

 if ("s".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1587).charCodeAt(), newKey:
String.fromCharCode(1587) };	  
  }
  if ("S".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1589).charCodeAt(), newKey:
String.fromCharCode(1589) };	  
  }
  if ("d".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1583).charCodeAt(), newKey:
String.fromCharCode(1583) };	  
  }
  if ("D".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1672).charCodeAt(), newKey:
String.fromCharCode(1672) };	  
  }
  if ("f".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1601).charCodeAt(), newKey:
String.fromCharCode(1601) };	  
  }
  if ("g".indexOf(key) != -1) {

    return { replaceKey: true, newKeyCode: String.fromCharCode(1711).charCodeAt(), newKey:
String.fromCharCode(1711) };	  
  }
  if ("G".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1594).charCodeAt(), newKey:
String.fromCharCode(1594) };	  
  }
  if ("h".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1726).charCodeAt(), newKey:
String.fromCharCode(1726) };	  
  }
  if ("H".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1581).charCodeAt(), newKey:
String.fromCharCode(1581) };	  
  }
  if ("j".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1580).charCodeAt(), newKey:
String.fromCharCode(1580) };	  
  }
  if ("J".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1590).charCodeAt(), newKey:
String.fromCharCode(1590) };	  
  }
  if ("k".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1705).charCodeAt(), newKey:
String.fromCharCode(1705) };	  
  }
  if ("K".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1582).charCodeAt(), newKey:
String.fromCharCode(1582) };	  
  }
  if ("l".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1604).charCodeAt(), newKey:
String.fromCharCode(1604) };	  
  }
  if ("L".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1554).charCodeAt(), newKey:
String.fromCharCode(1554) };	  
  }
  if ("z".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1586).charCodeAt(), newKey:
String.fromCharCode(1586) };	  
  }
  if ("Z".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1584).charCodeAt(), newKey:
String.fromCharCode(1584) };	  
  }
  if ("x".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1588).charCodeAt(), newKey:
String.fromCharCode(1588) };	  
  }
  if ("X".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1688).charCodeAt(), newKey:

String.fromCharCode(1688) };	  
  }
  if ("c".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1670).charCodeAt(), newKey:
String.fromCharCode(1670) };	  
  }
  if ("C".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1579).charCodeAt(), newKey:
String.fromCharCode(1579) };	  
  }
  if ("v".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1591).charCodeAt(), newKey:
String.fromCharCode(1591) };	  
  }
  if ("V".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1592).charCodeAt(), newKey:
String.fromCharCode(1592) };	  
  }
  if ("b".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1576).charCodeAt(), newKey:
String.fromCharCode(1576) };	  
  }
  if ("B".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1555).charCodeAt(), newKey:
String.fromCharCode(1555) };	  
  }

 if ("n".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1606).charCodeAt(), newKey:
String.fromCharCode(1606) };	  
  }
  if ("N".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1722).charCodeAt(), newKey:
String.fromCharCode(1722) };	  
  }
  if ("m".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1605).charCodeAt(), newKey:
String.fromCharCode(1605) };	  
  }
  if ("q".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1602).charCodeAt(), newKey:
String.fromCharCode(1602) };	  
  }
  if ("w".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1608).charCodeAt(), newKey:
String.fromCharCode(1608) };	  
  }
  if ("W".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(65018).charCodeAt(), newKey:
String.fromCharCode(65018) };	  
  }

 if ("e".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1593).charCodeAt(), newKey:
String.fromCharCode(1593) };	  
  }
  if ("E".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1553).charCodeAt(), newKey:
String.fromCharCode(1553) };	  
  }
  if ("r".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1585).charCodeAt(), newKey:
String.fromCharCode(1585) };	  
  }
  if ("R".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1681).charCodeAt(), newKey:
String.fromCharCode(1681) };	  
  }
  if ("t".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1578).charCodeAt(), newKey:
String.fromCharCode(1578) };	  
  }
  if ("T".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1657).charCodeAt(), newKey:
String.fromCharCode(1657) };	  
  }
  if ("y".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1746).charCodeAt(), newKey:
String.fromCharCode(1746) };	  
  }
  if ("Y".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1537).charCodeAt(), newKey:
String.fromCharCode(1537) };	  
  }
 if ("u".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1574).charCodeAt(), newKey:
String.fromCharCode(1574) };	  
  }
  if ("o".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1729).charCodeAt(), newKey:
String.fromCharCode(1729) };	  
  }
  if ("p".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1662).charCodeAt(), newKey:
String.fromCharCode(1662) };	  
  }
  if ("i".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1740).charCodeAt(), newKey:
String.fromCharCode(1740) };	  
  }
  if ("O".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1731).charCodeAt(), newKey:
String.fromCharCode(1731) };
  }
  if ("I".indexOf(key) != -1) {
	  //document.formName.texturdu.value=document.formName.texturdu.value + String.fromCharCode(1648);
    return { replaceKey: true, newKeyCode: String.fromCharCode(1648).charCodeAt(), newKey:
String.fromCharCode(1648) };
	  //return { cancelKey: true };
  }
  if ("$".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1569).charCodeAt(), newKey:
String.fromCharCode(1569) };
  }
 if ("0".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1776).charCodeAt(), newKey:
String.fromCharCode(1776) };
  }
 if ("1".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1777).charCodeAt(), newKey:
String.fromCharCode(1777) };
  }
 if ("2".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1778).charCodeAt(), newKey:
String.fromCharCode(1778) };
  }
 if ("3".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1779).charCodeAt(), newKey:
String.fromCharCode(1779) };
  }
 if ("4".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1780).charCodeAt(), newKey:
String.fromCharCode(1780) };
  }
 if ("5".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1781).charCodeAt(), newKey:
String.fromCharCode(1781) };
  }
 if ("6".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1782).charCodeAt(), newKey:
String.fromCharCode(1782) };
  }
 if ("7".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1783).charCodeAt(), newKey:
String.fromCharCode(1783) };
  }
 if ("8".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1784).charCodeAt(), newKey:
String.fromCharCode(1784) };
  }
 if ("9".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1785).charCodeAt(), newKey:
String.fromCharCode(1785) };
  }
  
/* these are arabic 
  if ("9".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1641).charCodeAt(), newKey:
String.fromCharCode(1641) };
  }
  if ("8".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1640).charCodeAt(), newKey:
String.fromCharCode(1640) };
  }
  if ("7".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1639).charCodeAt(), newKey:
String.fromCharCode(1639) };
  }
  if ("6".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1638).charCodeAt(), newKey:
String.fromCharCode(1638) };
  }
  if ("5".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1637).charCodeAt(), newKey:
String.fromCharCode(1637) };
  }
  if ("4".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1636).charCodeAt(), newKey:
String.fromCharCode(1636) };
  }
  if ("3".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1635).charCodeAt(), newKey:
String.fromCharCode(1635) };
  }
  if ("2".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1634).charCodeAt(), newKey:
String.fromCharCode(1634) };
  }
 if ("1".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1633).charCodeAt(), newKey:
String.fromCharCode(1633) };
  }
  if ("0".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1632).charCodeAt(), newKey:
String.fromCharCode(1632) };
  }
*/
  if (".".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1748).charCodeAt(), newKey:
String.fromCharCode(1748) };
  }    
  if ("'".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1748).charCodeAt(), newKey:
String.fromCharCode(1748) };
  }    
  if ("\"".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1600).charCodeAt(), newKey:
String.fromCharCode(1600) };
  }    
  
  if (";".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1563).charCodeAt(), newKey:
String.fromCharCode(1563) };
  } 
  if ("-".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1652).charCodeAt(), newKey:
String.fromCharCode(1652) };
  }      
  
  if ("P".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1615).charCodeAt(), newKey:
String.fromCharCode(1615) };
  }
  
  if ("<".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1616).charCodeAt(), newKey:
String.fromCharCode(1616) };
  }
  
  if (">".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1614).charCodeAt(), newKey:
String.fromCharCode(1614) };
  }
   
  if ("=".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1572).charCodeAt(), newKey:
String.fromCharCode(1572) };
  }   

  if ("*".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1612).charCodeAt(), newKey:
String.fromCharCode(1612) };
  }   
  if ("~".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1611).charCodeAt(), newKey:
String.fromCharCode(1611) };
  } 
  if ("`".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1613).charCodeAt(), newKey:
String.fromCharCode(1613) };
  }   
  if ("_".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1617).charCodeAt(), newKey:
String.fromCharCode(1617) };
  }    
  if ("Q".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1618).charCodeAt(), newKey:
String.fromCharCode(1618) };
  }    
  if ("/".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1618).charCodeAt(), newKey:
String.fromCharCode(1618) };
  }    

 if ("@".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1548).charCodeAt(), newKey:
String.fromCharCode(1548) };
  }   
 if (",".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1548).charCodeAt(), newKey:
String.fromCharCode(1548) };
  }   

 if ("#".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1549).charCodeAt(), newKey:
String.fromCharCode(1549) };
  }     
  if ("%".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1610).charCodeAt(), newKey:
String.fromCharCode(1610) };
  }     
  if ("^".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1552).charCodeAt(), newKey:
String.fromCharCode(1552) };
  }     
  if ("-".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1571).charCodeAt(), newKey:
String.fromCharCode(1571) };
  }     
  if ("U".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1623).charCodeAt(), newKey:
String.fromCharCode(1623) };
  }     
  if ("{".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(8216).charCodeAt(), newKey:
String.fromCharCode(8216) };
  }     
  if ("}".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(8217).charCodeAt(), newKey:
String.fromCharCode(8217) };
  }     
  if ("\\".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1550).charCodeAt(), newKey:
String.fromCharCode(1550) };
  }     
  if ("|".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1556).charCodeAt(), newKey:
String.fromCharCode(1556) };
  }     
  if ("F".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1622).charCodeAt(), newKey:
String.fromCharCode(1622) };
  }     
  if ("M".indexOf(key) != -1) {
    return { replaceKey: true, newKeyCode: String.fromCharCode(1573).charCodeAt(), newKey:
String.fromCharCode(1573) };
  }     

 else {
    return { cancelKey: false };
  }
}


