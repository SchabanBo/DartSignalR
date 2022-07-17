// File generated at 7/16/2022 3:10:27 PM
import 'package:flutter/foundation.dart';
import 'package:signalr_core/signalr_core.dart';

class ExampleHub{
   final HubConnection connection;
   const ExampleHub({ required this.connection});

   Future<String> A_User(AddUser command) async  => 
      await connection.invoke('A_User'  ,args: [command]) as String;

   Future<HubResult> U_User(UpdateUser command) async  => 
      HubResult.fromJson(await connection.invoke('U_User'  ,args: [command]) as Map<String,dynamic>);

   void D_User(DeleteUser id) => 
      connection.send(methodName: 'D_User'  ,args: [id]);

   void G_User(int id) => 
      connection.send(methodName: 'G_User'  ,args: [id]);

   void GA_User() => 
      connection.send(methodName: 'GA_User' );

   void A_Account(AddAccount command) => 
      connection.send(methodName: 'A_Account'  ,args: [command]);

   void U__Account(UpdateAccount command) => 
      connection.send(methodName: 'U__Account'  ,args: [command]);

   void D__Account(DeleteAccount id) => 
      connection.send(methodName: 'D__Account'  ,args: [id]);

   void G_Account(GetAccount id) => 
      connection.send(methodName: 'G_Account'  ,args: [id]);

   void GA_Account() => 
      connection.send(methodName: 'GA_Account' );



}

class AddUser{
   final String? firstName;
   final String? lastName;
   final int? age;
   final List<Address> addresses;

   const AddUser({this.firstName, this.lastName, this.age, this.addresses = const []});

   AddUser.fromJson(Map<String, dynamic> json):
       firstName = json['firstName'] == null ? null : json['firstName'] as String,
       lastName = json['lastName'] == null ? null : json['lastName'] as String,
       age = json['age'] == null ? null : json['age'] as int,
       addresses = json['addresses'] == null ? <Address>[] : List<Address>.from(json['addresses'].map((x) =>Address.fromJson(x as Map<String, dynamic>)) as Iterable);


   Map<String, dynamic> toJson() => {
      'firstName': firstName,
      'lastName': lastName,
      'age': age,
      'addresses': List<dynamic>.from(addresses.map((x) => x.toJson()))
   };

}

class HubResult{
   final Result result;
   final String? message;

   const HubResult({this.result = Result.Done, this.message});

   HubResult.fromJson(Map<String, dynamic> json):
       result = Result.values.firstWhere((f)=> describeEnum(f.toString()) == (json['result'] as String)),
       message = json['message'] == null ? null : json['message'] as String;


   Map<String, dynamic> toJson() => {
      'result': describeEnum(result),
      'message': message
   };

}

class UpdateUser{
   final int? id;
   final String? firstName;
   final String? lastName;
   final int? age;
   final List<Address> addresses;

   const UpdateUser({this.id, this.firstName, this.lastName, this.age, this.addresses = const []});

   UpdateUser.fromJson(Map<String, dynamic> json):
       id = json['id'] == null ? null : json['id'] as int,
       firstName = json['firstName'] == null ? null : json['firstName'] as String,
       lastName = json['lastName'] == null ? null : json['lastName'] as String,
       age = json['age'] == null ? null : json['age'] as int,
       addresses = json['addresses'] == null ? <Address>[] : List<Address>.from(json['addresses'].map((x) =>Address.fromJson(x as Map<String, dynamic>)) as Iterable);


   Map<String, dynamic> toJson() => {
      'id': id,
      'firstName': firstName,
      'lastName': lastName,
      'age': age,
      'addresses': List<dynamic>.from(addresses.map((x) => x.toJson()))
   };

}

class DeleteUser{
   final int? id;

   const DeleteUser({this.id});

   DeleteUser.fromJson(Map<String, dynamic> json):
       id = json['id'] == null ? null : json['id'] as int;


   Map<String, dynamic> toJson() => {
      'id': id
   };

}

class AddAccount{
   final String? userName;
   final String? email;
   final String? password;
   final List<String> rights;
   final DateTime? lastVisit;

   const AddAccount({this.userName, this.email, this.password, this.rights = const [], this.lastVisit});

   AddAccount.fromJson(Map<String, dynamic> json):
       userName = json['userName'] == null ? null : json['userName'] as String,
       email = json['email'] == null ? null : json['email'] as String,
       password = json['password'] == null ? null : json['password'] as String,
       rights = json['rights'] == null ? <String>[] : List<String>.from(json['rights'].map((x) =>x) as Iterable),
       lastVisit = json['lastVisit'] == null ? null : DateTime.parse(json['lastVisit'].toString()).toLocal();


   Map<String, dynamic> toJson() => {
      'userName': userName,
      'email': email,
      'password': password,
      'rights': List<dynamic>.from(rights.map((x) =>x)),
      'lastVisit': lastVisit?.toUtc().toIso8601String()
   };

}

class UpdateAccount{
   final int? id;
   final String? userName;
   final String? email;
   final String? password;
   final List<String> rights;
   final DateTime? lastVisit;

   const UpdateAccount({this.id, this.userName, this.email, this.password, this.rights = const [], this.lastVisit});

   UpdateAccount.fromJson(Map<String, dynamic> json):
       id = json['id'] == null ? null : json['id'] as int,
       userName = json['userName'] == null ? null : json['userName'] as String,
       email = json['email'] == null ? null : json['email'] as String,
       password = json['password'] == null ? null : json['password'] as String,
       rights = json['rights'] == null ? <String>[] : List<String>.from(json['rights'].map((x) =>x) as Iterable),
       lastVisit = json['lastVisit'] == null ? null : DateTime.parse(json['lastVisit'].toString()).toLocal();


   Map<String, dynamic> toJson() => {
      'id': id,
      'userName': userName,
      'email': email,
      'password': password,
      'rights': List<dynamic>.from(rights.map((x) =>x)),
      'lastVisit': lastVisit?.toUtc().toIso8601String()
   };

}

class DeleteAccount{
   final int? id;

   const DeleteAccount({this.id});

   DeleteAccount.fromJson(Map<String, dynamic> json):
       id = json['id'] == null ? null : json['id'] as int;


   Map<String, dynamic> toJson() => {
      'id': id
   };

}

class GetAccount{
   final int? id;

   const GetAccount({this.id});

   GetAccount.fromJson(Map<String, dynamic> json):
       id = json['id'] == null ? null : json['id'] as int;


   Map<String, dynamic> toJson() => {
      'id': id
   };

}

class Address{
   final String? city;
   final String? street;
   final String? number;

   const Address({this.city, this.street, this.number});

   Address.fromJson(Map<String, dynamic> json):
       city = json['city'] == null ? null : json['city'] as String,
       street = json['street'] == null ? null : json['street'] as String,
       number = json['number'] == null ? null : json['number'] as String;


   Map<String, dynamic> toJson() => {
      'city': city,
      'street': street,
      'number': number
   };

}

enum Result{
   Done,
   Error,
}


Duration parseDuration(String s) {
    var hours = 0;
    var minutes = 0;
    var micros = 0;
    List<String> parts = s.split(':');
    if (parts.length > 2) {
        hours = int.parse(parts[parts.length - 3]);
    }
    if (parts.length > 1) {
        minutes = int.parse(parts[parts.length - 2]);
    }
    micros = (double.parse(parts[parts.length - 1]) * 1000000).round();
    return Duration(hours: hours, minutes: minutes, microseconds: micros);
}
