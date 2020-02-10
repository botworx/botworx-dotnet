using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia
{
    public enum TokenKind
    {
        Nil, //PrimitivesBegin
        WhiteSpace,
        Comment,
        LeftRound,  //(
        RightRound, //)
        LeftSquare, //[
        RightSquare,//]
        LeftAngle,  //<
        RightAngle, //>

        //Operators Begin
        //Arrows Begin
        ShortSkinnyArrow,   //->
        LongSkinnyArrow,    //-->
        NotLongSkinnyArrow,    //!-->
        ShortFatArrow,      //=>
        LongFatArrow,       //==>
        NotLongFatArrow,       //!==>
        //Arrows End

        Equal,
        NotEqual,
        BangBang,
        Bang,
        PlusPlus,
        MinusMinus,
        MinusPlus,
        Plus,
        Minus,
        ForwardSlash,
        Tilde,
        Assign,
        At,
        //Operators End

        Dollar,
        //Pound,
        QuestionMark,
        Star,
        //Colon,

        //Literals Begin
        Float,
        Double,
        Integer,
        Boolean,
        //Literals End

        Type,
        Property,
        Directive,
        Name,
        Dot,
        Snippet,
        //
        Invalid,
        //PrimitivesEnd

        //KeywordsBegin
        Namespace,
        Brain,
        Using,
        PredicateKw,
        Expert,
        Context,
        Topic,
        Rule,
        Method,
        Task,
        Where,
        Post,
        Propose,
        Parallel,
        Do,
        Maybe,
        Weight,
        Select,
        Case,
        Condition,
        Precondition,
        Postcondition,
        Branch,
        Succeed,
        Fail,
        Throw,
        Halt,
        Return,
        //KeywordsEnd

        //DirectivesBegin
        PoundIndent,
        PoundDedent,
        //DirectivesEnd

        //MarkersBegin
        FileEnd,
        LineEnd,
        ExpressionEnd,
        Indent,
        Dedent,
        List, //Abstract
        LineList,
        ExpressionList, //Abstract
        RoundList,
        SquareList,
        DottedName,
        Variable,
        Literal,
        Fragment,
        Predicate,
        //MarkersEnd

        //Macros Begin
        SubjectMacro
        //Macros End
    }
}

[Flags]
public enum TokenCategory : int
{
    None = 0,
    Primitive = 1,
    Operator = 2,
    Arrow = 4,
    Literal = 8,
    Keyword = 16,
    Directive = 32,
    Marker = 64,
    Macro = 128
}